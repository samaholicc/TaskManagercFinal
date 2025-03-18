using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

public class TodoRepository
{
    private readonly string _connectionString;

    public TodoRepository(string connectionString = "Data Source=tasks.db;Version=3;")
    {
        _connectionString = connectionString;
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        if (!File.Exists("tasks.db"))
        {
            SQLiteConnection.CreateFile("tasks.db");
        }

        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Todos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    StartDate TEXT NOT NULL,
                    EndDate TEXT NOT NULL,
                    Status TEXT NOT NULL,
                    Priority INTEGER NOT NULL
                );";
                command.ExecuteNonQuery();
            }
        }
    }

    public List<Todo> GetAll()
    {
        var todos = new List<Todo>();
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Todos;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todos.Add(new Todo
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            StartDate = DateTime.Parse(reader.GetString(2)),
                            EndDate = DateTime.Parse(reader.GetString(3)),
                            Status = reader.GetString(4),
                            Priority = reader.GetInt32(5)
                        });
                    }
                }
            }
        }
        return todos;
    }

    public void Add(Todo todo)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Todos (Name, StartDate, EndDate, Status, Priority) VALUES ($Nom, $StartDate, $EndDate, $Status, $Priority); SELECT last_insert_rowid();";
                command.Parameters.AddWithValue("$Nom", todo.Name);
                command.Parameters.AddWithValue("$StartDate", todo.StartDate.ToString("o"));
                command.Parameters.AddWithValue("$EndDate", todo.EndDate.ToString("o"));
                command.Parameters.AddWithValue("$Status", todo.Status);
                command.Parameters.AddWithValue("$Priority", todo.Priority);
                todo.Id = Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }

    public void Update(Todo updatedTodo)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                UPDATE Todos
                SET Name = $Nom, StartDate = $StartDate, EndDate = $EndDate, Status = $Status, Priority = $Priority
                WHERE Id = $Id;";
                command.Parameters.AddWithValue("$Id", updatedTodo.Id);
                command.Parameters.AddWithValue("$Nom", updatedTodo.Name);
                command.Parameters.AddWithValue("$StartDate", updatedTodo.StartDate.ToString("o"));
                command.Parameters.AddWithValue("$EndDate", updatedTodo.EndDate.ToString("o"));
                command.Parameters.AddWithValue("$Status", updatedTodo.Status);
                command.Parameters.AddWithValue("$Priority", updatedTodo.Priority);
                command.ExecuteNonQuery();
            }
        }
    }

    public Todo GetById(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Todos WHERE Id = $id";
                command.Parameters.AddWithValue("$id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Todo
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            StartDate = DateTime.Parse(reader.GetString(2)),
                            EndDate = DateTime.Parse(reader.GetString(3)),
                            Status = reader.GetString(4),
                            Priority = reader.GetInt32(5)
                        };
                    }
                }
            }
        }
        return null;
    }

    public void Delete(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Todos WHERE Id = $id;";
                command.Parameters.AddWithValue("$id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
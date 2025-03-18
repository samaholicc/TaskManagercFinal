using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;



public class TodoRepository
{
    private readonly string _connectionString;

    public TodoRepository(string connectionString = "Data Source=todos.db;Version=3;")
    {
        _connectionString = connectionString;
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        if (!File.Exists("todos.db"))
        {
            SQLiteConnection.CreateFile("todos.db");
        }

        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            // Create the table if it does not exist
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Todo (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Date_Debut TEXT NOT NULL,
                    Date_Fin TEXT NOT NULL,
                    Statut TEXT NOT NULL,
                    Priorite INTEGER NOT NULL
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
                command.CommandText = "SELECT * FROM Todo;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var todo = new Todo
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Date_Debut = DateTime.Parse(reader.GetString(2)),
                            Date_Fin = DateTime.Parse(reader.GetString(3)),
                            Statut = reader.GetString(4),
                            Priorite = reader.GetInt32(5)
                        };
                        todos.Add(todo);
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
                command.CommandText = "INSERT INTO Todo (Name, Date_Debut, Date_fin, Statut, Priorite) VALUES ($Nom, $StartDate, $EndDate, $Status, $Priority);";
                command.Parameters.AddWithValue("$Nom", todo.Name);
                command.Parameters.AddWithValue("$StartDate", todo.Date_Debut.ToString("o"));
                command.Parameters.AddWithValue("$EndDate", todo.Date_Fin.ToString("o"));
                command.Parameters.AddWithValue("$Status", todo.Statut);
                command.Parameters.AddWithValue("$Priority", todo.Priorite);

                command.ExecuteNonQuery();
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
                UPDATE Todo
                SET Name = $Nom, Date_Debut = $DateDebut, Date_Fin = $EndDate, Statut = $Status, Priorite = $Priority
                WHERE Id = $Id;";

                command.Parameters.AddWithValue("$Id", updatedTodo.Id);
                command.Parameters.AddWithValue("$Nom", updatedTodo.Name);
                command.Parameters.AddWithValue("$DateDebut", updatedTodo.Date_Debut.ToString("o"));
                command.Parameters.AddWithValue("$EndDate", updatedTodo.Date_Fin.ToString("o"));
                command.Parameters.AddWithValue("$Status", updatedTodo.Statut);
                command.Parameters.AddWithValue("$Priority", updatedTodo.Priorite);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating Todo: {ex.Message}");
                    throw;
                }
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
                command.CommandText = "SELECT * FROM Todo WHERE Id = $id";
                command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Todo
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Date_Debut = reader.GetDateTime(2),
                            Date_Fin = reader.GetDateTime(3),
                            Statut = reader.GetString(4),
                            Priorite = reader.GetInt32(5)
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
                Console.WriteLine($"Attempting to delete Todo with ID: {id}");

                // Check if the Todo exists before trying to delete it
                var existsCommand = connection.CreateCommand();
                existsCommand.CommandText = "SELECT COUNT(*) FROM Todo WHERE Id = $id";
                existsCommand.Parameters.AddWithValue("$id", id);
                var count = Convert.ToInt32(existsCommand.ExecuteScalar());

                if (count == 0)
                {
                    throw new InvalidOperationException("Todo not found");
                }

                // Proceed with deletion
                command.CommandText = "DELETE FROM Todo WHERE Id = $id;";
                command.Parameters.AddWithValue("$id", id);
                command.ExecuteNonQuery();
            }
        }
    }


}

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

/// <summary>
/// Repository class for managing Todo items in SQLite database.
/// </summary>
public class TodoRepository
{
    private readonly string _connectionString;

    /// <summary>
    /// Constructor that initializes the database with an optional connection string.
    /// </summary>
    public TodoRepository(string connectionString = "Data Source=todos.db;Version=3;")
    {
        _connectionString = connectionString;
        InitializeDatabase();
    }

    /// <summary>
    /// Initializes the database by creating the database file and the Todo table if it does not exist.
    /// </summary>
    private void InitializeDatabase()
    {
        if (!File.Exists("todos.db"))
        {
            SQLiteConnection.CreateFile("todos.db");

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        CREATE TABLE Todo (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Date_debut TEXT NOT NULL,
                            Date_fin TEXT NOT NULL,
                            Statut TEXT NOT NULL,
                            Priorite INTEGER NOT NULL
                        );";
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    /// <summary>
    /// Retrieves all Todo items from the database.
    /// </summary>
    /// <returns>A list of Todo items.</returns>
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
                            Nom = reader.GetString(1), // Correction ici : Name au lieu de Nom
                            StartDate = DateTime.Parse(reader.GetString(2)),
                            EndDate = DateTime.Parse(reader.GetString(3)),
                            Status = reader.GetString(4),
                            Priority = reader.GetInt32(5)
                        };
                        todos.Add(todo);
                    }
                }
            }
        }

        return todos;
    }

    /// <summary>
    /// Adds a new Todo item to the database.
    /// </summary>
    /// <param name="newTodo">The new Todo item to add.</param>
    public void Add(Todo todo)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Todo (Nom, StartDate, EndDate, Status, Priority) VALUES ($Nom, $StartDate, $EndDate, $Status, $Priority);";
                command.Parameters.AddWithValue("$Nom", todo.Nom);
                command.Parameters.AddWithValue("$StartDate", todo.StartDate);
                command.Parameters.AddWithValue("$EndDate", todo.EndDate);
                command.Parameters.AddWithValue("$Status", todo.Status);
                command.Parameters.AddWithValue("$Priority", todo.Priority);

                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Updates an existing Todo item in the database.
    /// </summary>
    /// <param name="updatedTodo">The updated Todo item.</param>
    public void Update(Todo updatedTodo)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    UPDATE Todo
                    SET Name = $name, Date_debut = $date_debut, Date_fin = $date_fin, Statut = $statut, Priorite = $priorite
                    WHERE Id = $id;";

                command.Parameters.AddWithValue("$id", updatedTodo.Id);
                command.Parameters.AddWithValue("$name", updatedTodo.Nom);
                command.Parameters.AddWithValue("$date_debut", updatedTodo.StartDate.ToString("o"));
                command.Parameters.AddWithValue("$date_fin", updatedTodo.EndDate.ToString("o"));
                command.Parameters.AddWithValue("$statut", updatedTodo.Status);
                command.Parameters.AddWithValue("$priorite", updatedTodo.Priority);

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
                command.CommandText = "SELECT * FROM Todo WHERE Id = $id";
                command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Todo
                        {
                            Id = reader.GetInt32(0),
                            Nom = reader.GetString(1),
                            StartDate = reader.GetDateTime(2),
                            EndDate = reader.GetDateTime(3),
                            Status = reader.GetString(4),
                            Priority = reader.GetInt32(5)
                        };
                    }
                }
            }
        }
        return null; // If no Todo was found
    }

    /// <summary>
    /// Deletes a Todo item from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the Todo item to delete.</param>
    public void Delete(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                // Attempt to delete the Todo
                command.CommandText = "DELETE FROM Todo WHERE Id = $id;";
                command.Parameters.AddWithValue("$id", id);
                var rowsAffected = command.ExecuteNonQuery();

                // Check if no rows were affected
                if (rowsAffected == 0)
                {
                    throw new InvalidOperationException("Todo not found");
                }
            }
        }
    }

}

/// <summary>
/// Class representing a Todo item.
/// </summary>

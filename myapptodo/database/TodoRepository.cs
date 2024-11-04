using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

/// <summary>
/// Repository class for managing Todo items in SQLite database.
/// </summary>
public class TodoRepository
{
    // String de connexion à la base de données SQLite
    private const string ConnectionString = "Data Source=todos.db;Version=3;";

    /// <summary>
    /// Constructor that initializes the database.
    /// </summary>
    public TodoRepository()
    {
        InitializeDatabase();
    }

    /// <summary>
    /// Initializes the database by creating the database file and the Todo table if it does not exist.
    /// </summary>
    private void InitializeDatabase()
    {
        // Vérifier si le fichier de base de données existe déjà
        if (!File.Exists("todos.db"))
        {
            // Créer un nouveau fichier de base de données
            SQLiteConnection.CreateFile("todos.db");

            // Ouvrir la connexion à la base de données
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE Todo (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nom TEXT NOT NULL,
                        Date_debut TEXT NOT NULL,
                        Date_fin TEXT NOT NULL,
                        Statut TEXT NOT NULL,
                        Priorite INTEGER NOT NULL
                    );";
                // Exécute la commande pour créer la table
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Retrieves all Todo items from the database.
    /// </summary>
    /// <returns>A list of Todo items.</returns>
    public List<Todo> GetAll()
    {
        var todos = new List<Todo>(); // Liste pour stocker les tâches

        // Ouvrir une nouvelle connexion pour lire les données
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Todo;"; // Commande pour sélectionner toutes les tâches

            // Lire les résultats de la commande
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Créer une nouvelle instance de Todo à partir des résultats
                    var todo = new Todo
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        StartDate = DateTime.Parse(reader.GetString(2)),
                        EndDate = DateTime.Parse(reader.GetString(3)),
                        Status = reader.GetString(4),
                        Priority = reader.GetInt32(5)
                    };
                    todos.Add(todo); // Ajouter l'élément à la liste
                }
            }
        }

        return todos; // Retourner la liste de tâches
    }

    /// <summary>
    /// Adds a new Todo item to the database.
    /// </summary>
    /// <param name="newTodo">The new Todo item to add.</param>
    public void Add(Todo newTodo)
    {
        // Ouvrir une connexion à la base de données
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Todo (Nom, Date_debut, Date_fin, Statut, Priorite)
                VALUES ($nom, $date_debut, $date_fin, $statut, $priorite);";
            // Ajouter les paramètres de la commande
            command.Parameters.AddWithValue("$nom", newTodo.Name);
            command.Parameters.AddWithValue("$date_debut", newTodo.StartDate.ToString("o")); // Format ISO 8601
            command.Parameters.AddWithValue("$date_fin", newTodo.EndDate.ToString("o"));
            command.Parameters.AddWithValue("$statut", newTodo.Status);
            command.Parameters.AddWithValue("$priorite", newTodo.Priority);
            command.ExecuteNonQuery(); // Exécuter la commande d'insertion
        }
    }

    /// <summary>
    /// Updates an existing Todo item in the database.
    /// </summary>
    /// <param name="updatedTodo">The updated Todo item.</param>
    public void Update(Todo updatedTodo)
    {
        // Ouvrir une connexion à la base de données
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
            UPDATE Todo
            SET Nom = $nom, Date_debut = $date_debut, Date_fin = $date_fin, Statut = $statut,
            Priorite = $priorite
            WHERE Id = $id;"; // Mise à jour des colonnes de la tâche spécifiée par son ID
            // Ajout des paramètres de la commande
            command.Parameters.AddWithValue("$id", updatedTodo.Id); // ID de la tâche à mettre à jour
            command.Parameters.AddWithValue("$nom", updatedTodo.Name);
            command.Parameters.AddWithValue("$date_debut", updatedTodo.StartDate.ToString("o")); // Format ISO 8601
            command.Parameters.AddWithValue("$date_fin", updatedTodo.EndDate.ToString("o"));
            command.Parameters.AddWithValue("$statut", updatedTodo.Status);
            command.Parameters.AddWithValue("$priorite", updatedTodo.Priority);
            command.ExecuteNonQuery(); // Exécuter la commande de mise à jour
        }
    }

    /// <summary>
    /// Deletes a Todo item from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the Todo item to delete.</param>
    public void Delete(int id)
    {
        // Ouvrir une connexion à la base de données
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Todo WHERE Id = $id;"; // Commande pour supprimer la tâche par ID
            command.Parameters.AddWithValue("$id", id); // Ajouter l'ID de la tâche à supprimer
            command.ExecuteNonQuery(); // Exécuter la commande de suppression
        }
    }
}

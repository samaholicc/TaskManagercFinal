﻿using System.Data.SQLite; // Importation de la bibliothèque SQLite pour interagir avec des bases de données SQLite

namespace MyAppTodo // Déclaration d'un espace de noms pour organiser le code
{
    /// <summary>
    /// Classe responsable de la gestion de la base de données pour l'application Todo.
    /// </summary>
    public class Database
    {
        // Chaîne de connexion à la base de données SQLite, spécifiant le fichier de base de données
        private const string ConnectionString = "Data Source=tasks.db;Version=3;";

        /// <summary>
        /// Méthode pour initialiser la base de données.
        /// Crée la base de données et la table si elles n'existent pas déjà.
        /// </summary>
        public void InitializeDatabase()
        {
            // Création d'une connexion à la base de données avec la chaîne de connexion spécifiée
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open(); // Ouverture de la connexion à la base de données

                // Création d'une commande SQL
                var command = connection.CreateCommand();

                // Définition de la commande SQL pour créer la table "Todos"
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Todos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    Name TEXT NOT NULL, 
                    StartDate TEXT NOT NULL,
                    EndDate TEXT NOT NULL,
                    Status TEXT NOT NULL, 
                    Priority INTEGER NOT NULL 
                );";

                // Exécution de la commande pour créer la table dans la base de données
                command.ExecuteNonQuery();
            } // Le bloc 'using' garantit que la connexion sera fermée et disposée automatiquement
        }
    }
}

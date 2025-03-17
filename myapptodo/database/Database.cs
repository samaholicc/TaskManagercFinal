<<<<<<< HEAD
﻿using System;
using System.Configuration; // Ajouter l'using pour ConfigurationManager
using System.Data.SQLite;


namespace MyAppTodo
{
    public class Database
    {
        private static readonly string ConnectionString = ConfigurationManager.AppSettings["SQLiteConnectionString"];

        public void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
=======
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
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Todos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    Name TEXT NOT NULL, 
                    StartDate TEXT NOT NULL,
                    EndDate TEXT NOT NULL,
                    Status TEXT NOT NULL, 
                    Priority INTEGER NOT NULL 
                );";

<<<<<<< HEAD
                command.ExecuteNonQuery();
            }
=======
                // Exécution de la commande pour créer la table dans la base de données
                command.ExecuteNonQuery();
            } // Le bloc 'using' garantit que la connexion sera fermée et disposée automatiquement
>>>>>>> 1023f2c5db4adc178a50a92bc1a0e819b37783b9
        }
    }
}

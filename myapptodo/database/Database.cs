using System;
using System.Configuration; // Ajouter l'using pour ConfigurationManager
using System.Data.SQLite;

namespace MyAppTodo
{
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
                using (var command = connection.CreateCommand())
                {
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

                    // Exécution de la commande pour créer la table
                    command.ExecuteNonQuery();
                }
            } // La connexion se ferme automatiquement ici
        }
    }
}

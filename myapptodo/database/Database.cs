using System;
using System.Data.SQLite;
using System.IO;
//using System.Configuration; //Removed as no longer needed

namespace MyAppTodo.Database
{
    public class Database
    {
        private readonly string _connectionString;

        public Database()
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.db");
            _connectionString = $"Data Source={dbPath};Version=3;";
        }

        public void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
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
                        )";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
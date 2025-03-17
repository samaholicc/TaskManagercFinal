using System;
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
}

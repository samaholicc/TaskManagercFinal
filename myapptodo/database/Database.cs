using System;
using System.Data.SQLite;

namespace MyAppTodo.Database
{
    public class Database
    {
        private const string ConnectionString = "Data Source=tasks.db;Version=3;";

        public void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
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
    }
}
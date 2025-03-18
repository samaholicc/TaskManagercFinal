using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace MyAppTodo
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Initialize the database before running the form
            MyAppTodo.Database.Database db = new MyAppTodo.Database.Database();
            db.InitializeDatabase();

            // Show the database columns in the 'Todos' table
            ShowDatabaseColumns();

            // Now start the application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        // Method to retrieve and display columns from the 'Todos' table
        static void ShowDatabaseColumns()
        {
            string connectionString = "your_connection_string_here";  // Replace with your actual connection string
            string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Todos'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Debug.WriteLine("Columns in 'Todos' table:");
                        while (reader.Read())
                        {
                            string columnName = reader["COLUMN_NAME"].ToString();
                            Debug.WriteLine(columnName);  // Show each column name in the Output window
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No columns found or the table 'Todos' does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }
    }
}

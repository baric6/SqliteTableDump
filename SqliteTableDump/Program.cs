using System;
using System.Data.SQLite;

class Program
{
    static void Main(string[] args)
    {
        string databaseFile = "C:\\Users\\J.Hollon\\AppData\\Local\\Microsoft\\Edge\\User Data\\Default\\WebAssistDatabase";
        DumpAllTableColumns(databaseFile);
    }

    static void DumpAllTableColumns(string databaseFile)
    {
        try
        {
            // Create a connection to the SQLite database
            using (var connection = new SQLiteConnection($"Data Source={databaseFile};Version=3;"))
            {
                connection.Open();

                // Retrieve the table names from the database
                var tableNames = connection.GetSchema("Tables").Rows.Cast<System.Data.DataRow>()
                    .Select(row => row[2].ToString());

                // Iterate over the table names and retrieve their column information
                foreach (var tableName in tableNames)
                {
                    Console.WriteLine($"Table: '{tableName}'");
                    Console.WriteLine("Columns:");

                    using (var command = new SQLiteCommand($"PRAGMA table_info({tableName})", connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string columnName = reader.GetString(1);
                            Console.WriteLine(columnName);
                        }
                    }

                    Console.WriteLine();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("could not read......");
            Console.WriteLine(ex.Message);
        }
       
    }
}

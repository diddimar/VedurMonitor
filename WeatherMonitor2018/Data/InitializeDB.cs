using SQLitePCL;
using System;
using System.IO;

namespace WeatherMonitor2018.Data
{
    public static class InitializeDB
    {
        public static SQLiteConnection Connection
        {
            get
            {
                return connection;
            }
        }

        private static SQLiteConnection connection;
        private static string databaseFilePath;

        public static void Initialize()
        {
            string docs = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            databaseFilePath = Path.Combine(docs, "empty_sqlite.db");
            connection = new SQLiteConnection(databaseFilePath);
        }

        public static void CreateDatabaseAndTables()
        {
            if (File.Exists(databaseFilePath))
                return;

            using (connection)
            {
                // TODO: Configure init tables
            }
        }
    }
}

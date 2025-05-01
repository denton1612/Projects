using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Microsoft.Data.Sqlite;
using static System.Net.Mime.MediaTypeNames;

namespace BasketballPersistance .utils
{
    internal class DbUtils
    {
        private IDbConnection connection = null;
        private string database;

        public DbUtils(string database)
        {
            this.database = database;
        }

        private IDbConnection GetNewConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[database].ConnectionString;
            return new SqliteConnection(connectionString);
        }

        public IDbConnection GetConenction()
        {
            if (connection == null)
            {
                connection = GetNewConnection();
            }
            return connection;
        }
    }
}

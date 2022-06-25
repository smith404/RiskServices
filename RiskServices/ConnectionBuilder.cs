using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskServices
{
    public class ConnectionBuilder
    {

        public static string GetSQLConnectionString()
        {
            StringBuilder connectionString = new StringBuilder("Data Source=");

            connectionString.Append(Environment.GetEnvironmentVariable("sql-connection-data-source"));
            connectionString.Append(";Initial Catalog=");
            connectionString.Append(Environment.GetEnvironmentVariable("sql-connection-inital-catalogue"));
            connectionString.Append(";User Id=");
            connectionString.Append(Environment.GetEnvironmentVariable("sql-connection-user"));
            connectionString.Append(";Password=");
            connectionString.Append(Environment.GetEnvironmentVariable("sql-connection-password"));

            return connectionString.ToString();
        }
    }
}

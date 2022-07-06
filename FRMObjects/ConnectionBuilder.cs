using System;
using System.Text;

namespace FRMObjects
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
#if LOCAL
            connectionString.Append(";Password=");
            connectionString.Append(Environment.GetEnvironmentVariable("sql-connection-password"));
#else
            connectionString.Append(";Password=");
            connectionString.Append(Environment.GetEnvironmentVariable("sql-connection-password"));
#endif
            return connectionString.ToString();
        }

        public static string GetStorageAccountConnectionString()
        {
            StringBuilder connectionString = new StringBuilder("DefaultEndpointsProtocol=");

            connectionString.Append(Environment.GetEnvironmentVariable("storage-account-protocol"));
            connectionString.Append(";AccountName=");
            connectionString.Append(Environment.GetEnvironmentVariable("storage-account-name"));
            connectionString.Append(";AccountKey=");
            connectionString.Append(Environment.GetEnvironmentVariable("storage-account-key"));
#if LOCAL
            connectionString.Append(";BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");
#endif
            return connectionString.ToString();
        }

    }
}

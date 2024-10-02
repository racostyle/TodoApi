using Microsoft.Data.SqlClient;
using System.Data;

namespace Host.Sql
{
    public class SqlHandler
    {
        internal readonly string SQL_SERVER_NAME;
        internal readonly string DATABASE_NAME;

        public SqlHandler(string sqlServerName, string databaseName)
        {
            SQL_SERVER_NAME = sqlServerName;
            DATABASE_NAME = databaseName;
        }

        internal void EnsureDatabaseExists()
        {
            var connectionString = $"Data Source={SQL_SERVER_NAME};Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string dbCheckQuery = CommandsGen.EnsureDbExists(DATABASE_NAME);

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = dbCheckQuery;
                    command.ExecuteNonQuery();
                }
            }
        }

        internal void EnsureTableExists(string tableName)
        {
            var connectionString = $"Server={SQL_SERVER_NAME};Database={DATABASE_NAME};Integrated Security=True;TrustServerCertificate=True";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
                    connection.Open();
                    var tableExists = (int)command.ExecuteScalar();

                    if (tableExists == 0)
                    {
                        // Create table if not exists
                        using (IDbCommand createTableCommand = connection.CreateCommand())
                        {
                            createTableCommand.CommandText = CommandsGen.CreateTodoTableCommand(tableName);
                            createTableCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}

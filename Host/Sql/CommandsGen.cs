namespace Host.Sql
{
    public static class CommandsGen
    {
        internal static string CreateTodoTableCommand(string tableName)
        {
            return @$"
                CREATE TABLE {tableName}
                (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    CreatedDate DATETIME DEFAULT GETUTCDATE() NOT NULL,
                    DueDate DATETIME,
                    Creator NVARCHAR(100) NOT NULL,
                    Description NVARCHAR(255) NOT NULL,
                    Alert BIT DEFAULT 0 NOT NULL,
                    Extra1 NVARCHAR(255),
                    Extra2 NVARCHAR(255) ,
                    Extra3 NVARCHAR(255),
                    Extra4 NVARCHAR(255),
                    Extra5 NVARCHAR(255),
                )";
        }

        internal static string EnsureDbExists(string databaseName)
        {
            return $@"
                IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{databaseName}')
                BEGIN
                    CREATE DATABASE [{databaseName}]
                END";
        }
    }
}

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

        internal static string GenerateTestingObject(string tableName)
        {
            return @$"
                    INSERT INTO {tableName} (DueDate, Creator, Description, Alert, Extra1, Extra2, Extra3, Extra4, Extra5)
                    VALUES ('{DateTime.UtcNow.AddDays(7):yyyy-MM-dd HH:mm:ss}', 'TestUser', 'This is a test todo item.', 0, 
                    'Test Extra 1', 'Test Extra 2', 'Test Extra 3', 'Test Extra 4', 'Test Extra 5');
                 ";
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

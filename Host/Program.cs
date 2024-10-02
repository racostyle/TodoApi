
using Host.Auxiliary;
using Host.Sql;

namespace Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationHelper();

            var deleteAfter = new SqlHandler(config.GetSqlServerName, "TodoApi");
            deleteAfter.EnsureDatabaseExists();
            deleteAfter.EnsureTableExists("Todos");

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IConfigurationHelper>(config);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}


using Host.Auxiliary;

namespace Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationHelper();

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


using Host.Auxiliary;
using System.Net;
using System.Net.Sockets;

namespace Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var port = GetAvailablePort();
            var config = new ConfigurationHelper();

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IConfigurationHelper>(config);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(port);
            });

            var app = builder.Build();

            //app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        private static int GetAvailablePort()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
    }
}

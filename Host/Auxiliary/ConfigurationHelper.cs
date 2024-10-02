using System.Reflection;
using System.Text.Json;

namespace Host.Auxiliary
{
    public class ConfigurationHelper : IConfigurationHelper
    {
        private const string SETTINGS_NAME = "appsettings.Secrets.json";
        private readonly Dictionary<string, string> _configuration;

        public Dictionary<string, string> Configuration => _configuration;
        public string GetSqlServerName => _configuration["SqlServer"];

        public ConfigurationHelper()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string limit = assembly.GetName().Name;
            var result = LoadConfig(Directory.GetCurrentDirectory(), limit);

            if (!result.Available)
            {
                CreateSettings();
                throw new FileNotFoundException("The 'appsettings.Secrets.json' file was not found. A new settings file has been created. Please populate it with the necessary settings.");
            }

            var file = File.ReadAllText(FullSettingsName(result.Path));
            _configuration = JsonSerializer.Deserialize<Dictionary<string, string>>(file);
        }

        private (bool Available, string Path) LoadConfig(string path, string limit)
        {
            if (string.IsNullOrEmpty(path))
                return (false, "");

            var fullPath = FullSettingsName(path);
            if (File.Exists(fullPath))
                return (true, path);
            if (path.EndsWith(limit))
                return (false, "");
            return LoadConfig(new DirectoryInfo(path).Parent.FullName, limit);
        }

        private void CreateSettings()
        {
            var dict = new Dictionary<string, string>()
            {
                { "SqlServer","localhost"},
            };
            var text = JsonSerializer.Serialize(dict);
            File.WriteAllText(FullSettingsName(Directory.GetCurrentDirectory()), text);
        }

        private static string FullSettingsName(string path)
        {
            return Path.Combine(path, SETTINGS_NAME);
        }
    }
}

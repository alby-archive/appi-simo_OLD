namespace AppiSimo.Client.Middleware
{
    using System.IO;
    using System.Reflection;
    using Environment;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.JSInterop;

    public static class ConfigMiddleware
    {
        public static void AddConfiguration(this IServiceCollection services)
        {
            var config = GetConfiguration();
            
            services.AddSingleton(config);
            services.AddSingleton(config.AuthConfig);
        }
        
        static Configuration GetConfiguration()
        {
            // Get the configuration from embedded dll.
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("config.json"))
            using (var reader = new StreamReader(stream ?? throw new FileNotFoundException("config.json Not Found.")))
            {
                return Json.Deserialize<Configuration>(reader.ReadToEnd());
            }
        }
    }
}
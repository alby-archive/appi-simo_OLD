namespace AppiSimo.Client
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using Environment;
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Starting;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = GetConfiguration();
            var baseAddress = new Uri(configuration.ApiUrl);

            services.AddServices(configuration.CognitoClient);

            services.AddHttpClientFactory(baseAddress);
            services.AddEndPoints(baseAddress);
            services.AddValidators();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            SetCulture();
            app.AddComponent<App>("app");
        }

        static void SetCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("it-IT");
        }

        static Configuration GetConfiguration()
        {
            // Get the configuration from embedded dll.
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("config.json"))
            using (var reader = new StreamReader(stream ?? throw new FileNotFoundException("config.json Not Found.")))
            {
                return JsonConvert.DeserializeObject<Configuration>(reader.ReadToEnd());
            }
        }
    }
}
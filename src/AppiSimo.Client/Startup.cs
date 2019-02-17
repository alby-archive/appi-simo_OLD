namespace AppiSimo.Client
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net.Http;
    using System.Reflection;
    using Environment;
    using Microsoft.AspNetCore.Blazor.Browser.Http;
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.JSInterop;
    using Middelwares;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var config = GetConfiguration();       
            
            services.AddSingleton<HttpMessageHandler, BrowserHttpMessageHandler>();
            
            services.AddSingleton(sp => {
                var handler = sp.GetService<HttpMessageHandler>();
                var client = handler != null ? new HttpClient(handler) : new HttpClient();
                client.BaseAddress = new Uri(config.Api);
                return client;
            });

            services.AddEndPoints(config);
            services.AddValidatorModdleWare();
            services.AddRxServices();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            SetCulture();
            app.AddComponent<App>("app");
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

        static void SetCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("it-IT");
        }
    }
}
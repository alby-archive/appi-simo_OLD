namespace AppiSimo.Client
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Reflection;
    using Environment;
    using Microsoft.AspNetCore.Blazor.Browser.Http;
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Middleware;
    using Newtonsoft.Json;
    using Shared.Services;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = GetConfiguration();

            services.AddSingleton<HttpMessageHandler, BrowserHttpMessageHandler>();
            services.AddAuthServices(configuration.CognitoClient);

            services.AddSingleton(provider =>
            {
                var handler = provider.GetService<HttpMessageHandler>();
                var auth = provider.GetService<AuthService>();

                var client = handler != null ? new HttpClient(handler) : new HttpClient();

                client.BaseAddress = new Uri(configuration.ApiUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{auth?.CurrentUser?.IdToken ?? string.Empty}");

                return client;
            });

            services.AddEndPoints(configuration.ApiUrl);
            services.AddValidatorMiddleWare();
            services.AddRxServices();
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
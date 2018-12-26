namespace AppiSimo.Client
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Reflection;
    using AppiSimo.Shared.Model;
    using Clients;
    using Environment;
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.JSInterop;
    using Shared.Pages.Pager;
    using Shared.Pages.Searcher;
    using Shared.Services;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var config = GetConfiguration();

            var apiUri = new Uri(config.Api);

            services.AddSingleton(provider => new AppiSimoClient<User>(provider.GetRequiredService<HttpClient>(), apiUri));
            services.AddSingleton(provider => new AppiSimoClient<Event>(provider.GetRequiredService<HttpClient>(), apiUri));

            services.AddScoped<BaseRxService<Pager>, PagerService>();
            services.AddScoped<BaseRxService<Searcher>, SearcherService>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
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
    }
}
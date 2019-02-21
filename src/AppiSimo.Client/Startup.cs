namespace AppiSimo.Client
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using Microsoft.AspNetCore.Blazor.Browser.Http;
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.AspNetCore.Blazor.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Middelwares;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddSingleton(provider => new Configuration(new Uri(provider.GetRequiredService<IUriHelper>().GetBaseUri() + "api/")));

            services.AddSingleton<HttpMessageHandler, BrowserHttpMessageHandler>();
            
            services.AddSingleton(sp => {
                var handler = sp.GetService<HttpMessageHandler>();
                var client = handler != null ? new HttpClient(handler) : new HttpClient();
                client.BaseAddress = sp.GetRequiredService<Configuration>().Api;
                return client;
            });

            services.AddEndPoints();
            services.AddValidatorModdleWare();
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
    }
}
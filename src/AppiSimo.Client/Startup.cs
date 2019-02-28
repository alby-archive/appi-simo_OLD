namespace AppiSimo.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Net.Http;
    using System.Reflection;
    using Environment;
    using Microsoft.AspNetCore.Blazor.Browser.Http;
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.AspNetCore.Blazor.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.JSInterop;
    using Middleware;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {        
            services.AddSingleton<HttpMessageHandler, BrowserHttpMessageHandler>();
            
            services.AddSingleton(provider => {
                var handler = provider.GetService<HttpMessageHandler>();
                var client = handler != null ? new HttpClient(handler) : new HttpClient();
                client.BaseAddress = new Uri(provider.GetRequiredService<Configuration>().ApiUrl);
                return client;
            });     
            
            services.AddConfiguration();
            services.AddEndPoints();
            services.AddValidatorMiddleWare();
            services.AddRxServices();
            services.AddAuthServices();
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
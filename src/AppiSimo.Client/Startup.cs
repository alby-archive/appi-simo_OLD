namespace AppiSimo.Client
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Environment;
    using Microsoft.AspNetCore.Blazor.Browser.Http;
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Middleware;
    using Pages.CurrentUserBadge;
    using Shared.Services;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HttpMessageHandler, BrowserHttpMessageHandler>();
            services.AddAuthServices();
            
            services.AddSingleton(provider =>
            {
                var handler = provider.GetService<HttpMessageHandler>();
                var auth = provider.GetService<AuthService>();

                var client = handler != null ? new HttpClient(handler) : new HttpClient();
                
                client.BaseAddress = new Uri(provider.GetRequiredService<Configuration>().ApiUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{auth?.CurrentUser?.id_token ?? string.Empty}");
                
                return client;
            });

            services.AddSingleton<CurrentUserBadgeViewModel>();

            services.AddConfiguration();
            services.AddEndPoints();
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
    }
}
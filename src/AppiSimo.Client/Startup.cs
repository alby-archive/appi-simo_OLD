namespace AppiSimo.Client
{
    using System;
    using System.Net.Http;
    using Clients;
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider => new AppiSimoClient(provider.GetRequiredService<HttpClient>(), new Uri("http://localhost:5002/odata/")));
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
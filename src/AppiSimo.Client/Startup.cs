namespace AppiSimo.Client
{
    using System;
    using System.Net.Http;
    using Clients;
    using Microsoft.AspNetCore.Blazor.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Shared.Pages.Pager;
    using Shared.Pages.Searcher;
    using Shared.Services;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider => new AppiSimoClient(provider.GetRequiredService<HttpClient>(), new Uri("http://localhost:5002/odata/")));
            
            services.AddScoped<BaseRxService<Pager>, PagerService>();
            services.AddScoped<BaseRxService<Searcher>, SearcherService>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
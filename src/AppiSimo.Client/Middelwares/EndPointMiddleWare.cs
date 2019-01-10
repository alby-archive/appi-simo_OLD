namespace AppiSimo.Client.Middelwares
{
    using System;
    using System.Net.Http;
    using AppiSimo.Shared.Model;
    using EndPoints;
    using Environment;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OData.Client;

    public static class EndPointMiddleWare
    {
        public static void AddEndPoints(this IServiceCollection services, Configuration config)
        {
            var baseUri = new Uri(config.Api);
            
            var context = new DataServiceContext(baseUri);

            services.AddSingleton(provider => new EndPoint<User>(context, provider.GetRequiredService<HttpClient>(), "users"));
            services.AddSingleton(provider => new EndPoint<Event>(context, provider.GetRequiredService<HttpClient>(), "events"));
            services.AddSingleton(provider => new EndPoint<Court>(context, provider.GetRequiredService<HttpClient>(), "courts"));
        }
    }
}
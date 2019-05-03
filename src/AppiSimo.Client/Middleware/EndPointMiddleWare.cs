namespace AppiSimo.Client.Middleware
{
    using System;
    using System.Net.Http;
    using AppiSimo.Shared.Abstract;
    using AppiSimo.Shared.Model;
    using EndPoints;
    using Environment;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OData.Client;

    public static class EndPointMiddleWare
    {
        public static void AddEndPoints(this IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var api = new Uri(provider.GetRequiredService<Configuration>().ApiUrl);
                return new DataServiceContext(api);
            });

            services.AddSingleton(provider => CreateEndPoint<Event>(provider, "events"));
            services.AddSingleton(provider => CreateEndPoint<Court>(provider, "courts"));
            services.AddSingleton(provider => CreateEndPoint<Light>(provider, "lights"));
            services.AddSingleton(provider => CreateEndPoint<Heat>(provider, "heats"));
            services.AddSingleton(provider => CreateEndPoint<UserEvent>(provider, "userEvent"));
            services.AddSingleton(provider => CreateEndPoint<User>(provider, "users"));

            services.AddSingleton(provider => new UserEndPoint(provider.GetRequiredService<DataServiceContext>(), provider.GetRequiredService<HttpClient>(), "users"));
        }

        static EndPoint<T> CreateEndPoint<T>(IServiceProvider provider, string uri)
            where T : class, IEntity, new() =>
            new EndPoint<T>(provider.GetRequiredService<DataServiceContext>(), provider.GetRequiredService<HttpClient>(), uri);
    }
}
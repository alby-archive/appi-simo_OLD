namespace AppiSimo.Client.Middelwares
{
    using System;
    using System.Net.Http;
    using AppiSimo.Shared.Abstract;
    using AppiSimo.Shared.Model;
    using EndPoints;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OData.Client;

    public static class EndPointMiddleWare
    {
        public static void AddEndPoints(this IServiceCollection services)
        {            
            services.AddSingleton(sp => new DataServiceContext(sp.GetRequiredService<Configuration>().Api));

            services.AddSingleton(provider => CreateEndPoint<Event>(provider, "events"));
            services.AddSingleton(provider => CreateEndPoint<Court>(provider, "courts"));
            services.AddSingleton(provider => CreateEndPoint<Light>(provider, "lights"));
            services.AddSingleton(provider => CreateEndPoint<Heat>(provider, "heats"));
            services.AddSingleton(provider => CreateEndPoint<UserEvent>(provider, "userEvent"));
            services.AddSingleton(provider => CreateEndPoint<User>(provider, "users"));
            
            services.AddSingleton(provider => new UserEndPoint(provider.GetRequiredService<DataServiceContext>(), provider.GetRequiredService<HttpClient>(), "users"));
        }

        static EndPoint<T> CreateEndPoint<T>(IServiceProvider provider, string name)
            where T : class, IEntity, new() 
            => new EndPoint<T>(provider.GetRequiredService<DataServiceContext>(), provider.GetRequiredService<HttpClient>(), name);
    }
}
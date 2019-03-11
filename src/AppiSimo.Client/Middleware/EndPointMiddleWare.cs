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
    using Shared.Services;

    public static class EndPointMiddleWare
    {
        public static void AddEndPoints(this IServiceCollection services)
        {            
            services.AddSingleton(provider =>
            {
                var api = new Uri(provider.GetRequiredService<Configuration>().ApiUrl);
                var context = new DataServiceContext(api);
                var authService = provider.GetRequiredService<AuthService>();
                
                context.BuildingRequest += (_, args) =>
                {
                    var user = authService.User.Value;
                    if (user != null)
                    {
                        args.Headers.Add("Authorization", $"Bearer {user.Token.Value}");
                    }
                };
                
                return context;
            });

            services.AddSingleton(provider => CreateEndPoint<Event>(provider, "events"));
            services.AddSingleton(provider => CreateEndPoint<Court>(provider, "courts"));
            services.AddSingleton(provider => CreateEndPoint<Light>(provider, "lights"));
            services.AddSingleton(provider => CreateEndPoint<Heat>(provider, "heats"));
            services.AddSingleton(provider => CreateEndPoint<UserEvent>(provider, "userEvent"));
            services.AddSingleton(provider => CreateEndPoint<User>(provider, "users"));
            
            services.AddSingleton(provider => new UserEndPoint(provider.GetRequiredService<DataServiceContext>(), provider.GetRequiredService<HttpClient>(), provider.GetRequiredService<AuthService>(), "users"));
        }

        static EndPoint<T> CreateEndPoint<T>(IServiceProvider provider, string name)
            where T : class, IEntity, new() 
            => new EndPoint<T>(provider.GetRequiredService<DataServiceContext>(), provider.GetRequiredService<HttpClient>(), provider.GetRequiredService<AuthService>(), name);
    }
}
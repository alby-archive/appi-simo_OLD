namespace AppiSimo.Client.Starting
{
    using System;
    using System.Net.Http;
    using AppiSimo.Shared.Abstract;
    using AppiSimo.Shared.Model;
    using AppiSimo.Shared.Validators;
    using EndPoints;
    using Environment;
    using Factories;
    using Factories.Abstract;
    using Microsoft.AspNetCore.Blazor.Browser.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OData.Client;
    using Shared.Pages.Pager;
    using Shared.Pages.Searcher;
    using Shared.Services;
    using Shared.Services.Abstract;

    public static class StartupProviders
    {
        public static void AddHttpClientFactory(this IServiceCollection services)
        {
            services.AddSingleton<HttpMessageHandler, BrowserHttpMessageHandler>();

            services.AddSingleton<IAsyncFactory<HttpClient>, HttpClientAsyncFactory>();
        }

        public static void AddEndPoints(this IServiceCollection services)
        {
            // ODataEndPoint
            services.AddSingleton(provider => new DataServiceContext(provider.GetRequiredService<Api>().Url));

            // ApiEndPoints
            services.AddSingleton(provider => CreateEndPoint<Event>(provider, "events"));
            services.AddSingleton(provider => CreateEndPoint<Court>(provider, "courts"));
            services.AddSingleton(provider => CreateEndPoint<Light>(provider, "lights"));
            services.AddSingleton(provider => CreateEndPoint<Heat>(provider, "heats"));
            services.AddSingleton(provider => CreateEndPoint<UserEvent>(provider, "userEvent"));

            services.AddSingleton(provider =>
            {
                var context = provider.GetService<DataServiceContext>();
                var factory = provider.GetService<IAsyncFactory<HttpClient>>();

                return new UserEndPoint(context, factory, "users");
            });
        }

        static EndPoint<T> CreateEndPoint<T>(IServiceProvider provider, string endPointName)
            where T : class, IEntity, new()
        {
            var context = provider.GetService<DataServiceContext>();
            var client = provider.GetService<IAsyncFactory<HttpClient>>();

            return new EndPoint<T>(context, client, endPointName);
        }

        public static void AddValidators(this IServiceCollection builder)
        {
            builder.AddSingleton(_ => ValidatorProxy.EventsValidator);
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IAsyncFactory<IAuthService>, AuthServiceAsyncFactory>();

            services.AddTransient<BaseRxService<Pager>, PagerService>();
            services.AddTransient<BaseRxService<Searcher>, SearcherService>();
        }
    }
}
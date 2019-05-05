namespace AppiSimo.Client.Starting
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using AppiSimo.Shared.Abstract;
    using AppiSimo.Shared.Model;
    using AppiSimo.Shared.Validators;
    using EndPoints;
    using Environment;
    using Microsoft.AspNetCore.Blazor.Browser.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OData.Client;
    using Shared.Pages.Pager;
    using Shared.Pages.Searcher;
    using Shared.Services;
    using Shared.Services.Abstract;

    public static class StartupProviders
    {
        public static void AddHttpClient(this IServiceCollection services, Uri baseUrl)
        {
            services.AddSingleton<HttpMessageHandler, BrowserHttpMessageHandler>();

            services.AddSingleton(provider =>
            {
                var handler = provider.GetService<HttpMessageHandler>();
                var client = handler != null ? new HttpClient(handler) : new HttpClient();

                var auth = provider.GetService<IAuthService>();

                client.BaseAddress = baseUrl;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{auth?.CurrentUser?.IdToken ?? "NULL"}");

                return client;
            });
        }

        public static void AddEndPoints(this IServiceCollection services, Uri baseUrl)
        {
            // ODataEndPoint
            services.AddSingleton(provider => new DataServiceContext(baseUrl));

            // ApiEndPoints
            services.AddSingleton(provider => CreateEndPoint<Event>(provider, "events"));
            services.AddSingleton(provider => CreateEndPoint<Court>(provider, "courts"));
            services.AddSingleton(provider => CreateEndPoint<Light>(provider, "lights"));
            services.AddSingleton(provider => CreateEndPoint<Heat>(provider, "heats"));
            services.AddSingleton(provider => CreateEndPoint<UserEvent>(provider, "userEvent"));

            services.AddSingleton(provider =>
            {
                var context = provider.GetService<DataServiceContext>();
                var client = provider.GetService<HttpClient>();

                return new UserEndPoint(context, client, "users");
            });
        }

        static EndPoint<T> CreateEndPoint<T>(IServiceProvider provider, string endPointName)
            where T : class, IEntity, new()
        {
            var context = provider.GetService<DataServiceContext>();
            var client = provider.GetService<HttpClient>();

            return new EndPoint<T>(context, client, endPointName);
        }

        public static void AddValidators(this IServiceCollection builder)
        {
            builder.AddSingleton(_ => ValidatorProxy.EventsValidator);
        }

        public static void AddServices(this IServiceCollection services, CognitoClient config)
        {
            services.AddSingleton<IAuthService>(_ => new AuthService(config));

            services.AddTransient<BaseRxService<Pager>, PagerService>();
            services.AddTransient<BaseRxService<Searcher>, SearcherService>();
        }
    }
}
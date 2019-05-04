namespace AppiSimo.Client.Middleware
{
    using AppiSimo.Shared.Environment;
    using Microsoft.Extensions.DependencyInjection;
    using Shared.Pages.Pager;
    using Shared.Pages.Searcher;
    using Shared.Services;

    public static class ServiceMiddleWare
    {
        public static void AddRxServices(this IServiceCollection services)
        {
            services.AddTransient<BaseRxService<Pager>, PagerService>();
            services.AddTransient<BaseRxService<Searcher>, SearcherService>();
        }

        public static void AddAuthServices(this IServiceCollection services, CognitoClient client)
        {
            services.AddSingleton(_ => new AuthService(client));
        }
    }
}
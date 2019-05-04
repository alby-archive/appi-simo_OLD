namespace AppiSimo.Client.Middleware
{
    using Environment;
    using Microsoft.Extensions.DependencyInjection;
    using Shared.Pages.Pager;
    using Shared.Pages.Searcher;
    using Shared.Services;
    using Shared.Services.Abstract;

    public static class ServiceMiddleWare
    {
        public static void AddRxServices(this IServiceCollection services)
        {
            services.AddTransient<BaseRxService<Pager>, PagerService>();
            services.AddTransient<BaseRxService<Searcher>, SearcherService>();
        }

        public static void AddAuthServices(this IServiceCollection services, CognitoClient client)
        {
            services.AddSingleton<IAuthService>(_ => new AuthService(client));
        }
    }
}
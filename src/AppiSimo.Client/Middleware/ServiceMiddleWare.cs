namespace AppiSimo.Client.Middleware
{
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

        public static void AddAuthServices(this IServiceCollection services)
        {
            services.AddSingleton<AuthService>();
        }
    }
}
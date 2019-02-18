namespace AppiSimo.Client.Middelwares
{
    using Microsoft.Extensions.DependencyInjection;
    using Shared.Pages.Pager;
    using Shared.Pages.Searcher;
    using Shared.Services;

    public static class RxServiceMiddleWare
    {
        public static void AddRxServices(this IServiceCollection services)
        {
            services.AddTransient<BaseRxService<Pager>, PagerService>();
            services.AddTransient<BaseRxService<Searcher>, SearcherService>();
        }
    }
}
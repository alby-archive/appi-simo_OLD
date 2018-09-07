namespace AppiSimo.Client.Shared.Pages.Searcher
{
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services;

    public class SearcherComponent : BlazorComponent
    {
        [Parameter]
        BaseRxService<Searcher> SearcherService { get; set; }

        protected void OnFilterChanged(UIChangeEventArgs args)
        {
            SearcherService.OnNext(new Searcher(args.Value.ToString()));
        }
    }
}
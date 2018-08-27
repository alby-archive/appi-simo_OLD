namespace AppiSimo.Client.Pages.Pager
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;

    public abstract class PagerBaseComponent : BlazorComponent
    {
        protected int TotalItems { get; set; }

        protected int CurrentPage { get; private set; }

        protected int PageSize => 20;

        protected async Task PagerFiredEvent(int currentPage)
        {
            CurrentPage = currentPage;
            await PageChangedHandler();
            StateHasChanged();
        }

        protected abstract Task PageChangedHandler();
    }
}
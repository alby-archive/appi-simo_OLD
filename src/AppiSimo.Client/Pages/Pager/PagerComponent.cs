namespace AppiSimo.Client.Pages.Pager
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;

    public class PagerComponent : BlazorComponent
    {
        [Parameter]
        int TotalItems { get; set; }

        [Parameter]
        int PageSize { get; set; }

        [Parameter]
        int CurrentPage { get; set; }

        [Parameter]
        Func<int, Task> CurrentPageChanged { get; set; }

        protected bool DisablePrevius { get; private set; } = true;
        protected bool DisableNext { get; private set; }

        protected override Task OnParametersSetAsync()
        {
            DisabledButtons();
            return base.OnParametersSetAsync();
        }

        protected async Task Next()
        {
            await CurrentPageChanged(++CurrentPage);
            DisabledButtons();
        }

        protected async Task Previous()
        {
            await CurrentPageChanged(--CurrentPage);
            DisabledButtons();
        }

        void DisabledButtons()
        {
            DisableNext = (PageSize != 0 && CurrentPage >= (TotalItems - 1) / PageSize) || TotalItems == 0;
            DisablePrevius = CurrentPage == 0;
        }
    }
}
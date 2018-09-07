namespace AppiSimo.Client.Shared.Pages.Pager
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services;

    public class PagerComponent : BlazorComponent
    {
        int _totalItems;

        [Parameter]
        int TotalItems
        {
            set
            {
                if (_totalItems == value)
                {
                    return;
                }

                _totalItems = value;

                DisabledButtons();
                StateHasChanged();
            }
        }

        [Parameter]
        BaseRxService<Pager> PagerService { get; set; }

        protected bool DisablePrevious { get; private set; } = true;
        protected bool DisableNext { get; private set; }

        protected override Task OnParametersSetAsync()
        {
            DisabledButtons();
            return base.OnParametersSetAsync();
        }

        protected void Next()
        {
            PagerService.OnNext(new Pager { CurrentPage = ++PagerService.Value.CurrentPage });
            DisabledButtons();
        }

        protected void Previous()
        {
            PagerService.OnNext(new Pager { CurrentPage = --PagerService.Value.CurrentPage });
            DisabledButtons();
        }

        void DisabledButtons()
        {
            DisableNext = PagerService.Value.PageSize != 0 && PagerService.Value.CurrentPage >= (_totalItems - 1) / PagerService.Value.PageSize || _totalItems == 0;
            DisablePrevious = PagerService.Value.CurrentPage == 0;
        }
    }
}
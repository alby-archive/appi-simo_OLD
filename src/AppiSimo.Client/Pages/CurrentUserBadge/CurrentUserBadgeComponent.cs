namespace AppiSimo.Client.Pages.CurrentUserBadge
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Shared.Services;

    public class CurrentUserBadgeComponent : BlazorComponent, IDisposable
    {
        IDisposable _subscription;
        
        [Inject]
        AuthService Service { get; set; }
        
        [Inject]
        IUriHelper UriHelper { get; set; }
        
        protected CurrentUserBadgeViewModel ViewModel { get; } = new CurrentUserBadgeViewModel();

        protected override void OnInit()
        {
            ViewModel.IsLogged = Service.CurrentUser != null;
            ViewModel.CurrentUser = Service?.CurrentUser?.Profile?.Username?? "User";

            _subscription =
                Service.User.Subscribe(user =>
                {
                    ViewModel.CurrentUser = user?.Profile.Username ?? "User";
                    ViewModel.IsLogged = user != null;

                    StateHasChanged();
                });
        }

        protected async Task SignIn() => await Service.SignIn();

        protected void SignOut()
        {
            Service.SignOut();
            
            StateHasChanged();
        }

        protected void GoToDetail() => UriHelper.NavigateTo($"/user/{Service.CurrentUser.Profile.Sub}");

        public void Dispose()
        {
            _subscription.Dispose();
        }
    }
}
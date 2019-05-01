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
        
        [Inject]
        protected CurrentUserBadgeViewModel ViewModel { get; set; }

        protected override void OnInit()
        {
            _subscription =
                Service.User.Subscribe(user =>
                {
                    ViewModel.CurrentUser = user?.profile.username ?? "User";
                    StateHasChanged();
                });
        }

        protected async Task SignIn() => await Service.SignIn();

        protected void SignOut()
        {
            Service.SignOut();
            
            StateHasChanged();
        }

        protected void GoToDetail() => UriHelper.NavigateTo($"/user/{Service.CurrentUser.profile.sub}");

        public void Dispose()
        {
            _subscription.Dispose();
        }
    }
}
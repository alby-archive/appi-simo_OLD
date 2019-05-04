namespace AppiSimo.Client.Pages.CurrentUserBadge
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Shared.Services.Abstract;

    public class CurrentUserBadgeComponent : BlazorComponent, IDisposable
    {
        IDisposable _subscription;

        [Inject]
        IAuthService Auth { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected CurrentUserBadgeViewModel ViewModel { get; } = new CurrentUserBadgeViewModel();

        protected override void OnInit()
        {
            ViewModel.IsLogged = Auth.CurrentUser != null;
            ViewModel.CurrentUser = Auth?.CurrentUser?.Profile?.Username ?? "User";

            _subscription =
                Auth.User.Subscribe(user =>
                {
                    ViewModel.CurrentUser = user?.Profile.Username ?? "User";
                    ViewModel.IsLogged = user != null;

                    StateHasChanged();
                });
        }

        protected async Task SignIn() => await Auth.SignIn();

        protected void SignOut()
        {
            Auth.SignOut();

            StateHasChanged();
        }

        protected void GoToDetail() => UriHelper.NavigateTo($"/user/{Auth.CurrentUser.Profile.Sub}");

        public void Dispose()
        {
            _subscription.Dispose();
        }
    }
}
namespace AppiSimo.Client.Pages.CurrentUserBadge
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Shared.Services;

    public class CurrentUserBadgeComponent : BlazorComponent
    {
        [Inject]
        protected AuthService Service { get; set; }

        protected string CurrentUser = "Login1"; 

        protected override void OnInit()
        {
            Service.Profile.Subscribe(user =>
            {
                CurrentUser = user?.username ?? "Login2";
                StateHasChanged();
            });
        }

        protected async Task SignIn() => await Service.SignIn();

        protected void SignOut() => Service.SignOut();

        protected bool IsUserLogged() => Service.CurrentProfile != null;
    }
}
namespace AppiSimo.Client.Pages.CurrentUserBadge
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Shared.Services;

    public class CurrentUserBadgeComponent : BlazorComponent
    {
        [Inject]
        protected AuthService AuthService { get; set; }

        [Inject]
        protected IUriHelper UriHelper { get; set; }

        protected override Task OnInitAsync()
        {
            AuthService.User.Subscribe(_ => StateHasChanged());
            return Task.CompletedTask;
        }

        protected void SignOut() => AuthService.SignOut();

        protected void SignIn() => UriHelper.NavigateTo("login");

        protected void GoToProfile() => UriHelper.NavigateTo("user");
    }
}
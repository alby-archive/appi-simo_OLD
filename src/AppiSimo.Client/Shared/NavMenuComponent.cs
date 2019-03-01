namespace AppiSimo.Client.Shared
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Services;

    public class NavMenuComponent :BlazorComponent
    {      
        [Inject]
        protected AuthService AuthService { get; set; }

        protected override Task OnInitAsync()
        {
            AuthService.User.Subscribe(_ => StateHasChanged());
            return Task.CompletedTask;
        }

        protected void SignOut() 
            => AuthService.SignOut();
    }
}
namespace AppiSimo.Client.Pages.Auth.SignedIn
{
    using System;
    using System.Threading.Tasks;
    using EndPoints;
    using Microsoft.AspNetCore.Blazor.Components;
    using Shared.Services;

    public class SignedInComponent : BlazorComponent
    {
        [Inject]
        AuthService AuthService { get; set; }

        protected override async Task OnInitAsync()
        {
            await AuthService.SignedIn();
        }
    }
}
namespace AppiSimo.Client.Pages.Auth.Login
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Shared.Model;
    using Shared.Services;

    public class LoginComponent : BlazorComponent
    {
        [Inject]
        AuthService AuthService { get; set; }
        
        [Inject]
        IUriHelper UriHelper { get; set; }
        
        protected AuthUser AuthUser { get; set; } = new AuthUser();

        protected async Task SignIn()
        {
            Console.WriteLine($"HEI");
            
            var token = await AuthService.SignIn(AuthUser);

            Console.WriteLine($"TOKEN: {token}");
            
            if (token == "newPasswordRequired")
            {
                UriHelper.NavigateTo("new-password");
            }
        }
    }
}
namespace AppiSimo.Client.Pages.Auth.NewPassword
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Shared.Model;
    using Shared.Services;

    public class NewPasswordComponent : BlazorComponent
    {
        [Parameter]
        string Username { get; set; }

        protected string OldPassword { get; set; }
        
        protected string NewPassword { get; set; }
        
        [Inject]
        AuthService AuthService { get; set; }

        protected async Task Change()
        {
            await AuthService.ChangePassword(Username, OldPassword, NewPassword);
            await AuthService.SignIn(Username, NewPassword);
            
            Console.WriteLine("TOKEN: " + AuthService.User.Value.Token.Value);
        }
    }
}
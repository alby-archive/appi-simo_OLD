namespace AppiSimo.Client.Pages.Auth.NewPassword
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Shared.Model;
    using Shared.Services;

    public class NewPasswordComponent : BlazorComponent
    {
        [Inject]
        AuthService AuthService { get; set; }
        
        protected Password Password { get; set; } = new Password();
        
        protected async Task Change()
        {
            await AuthService.ChangePassword(Password);            
        }
    }
}
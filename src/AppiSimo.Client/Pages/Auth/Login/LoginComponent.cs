namespace AppiSimo.Client.Pages.Auth.Login
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Shared.Services;

    public class LoginComponent : BlazorComponent
    {
        [Inject]
        AuthService AuthService { get; set; }
        
        [Inject]
        IUriHelper UriHelper { get; set; }

        protected string Username { get; set; }

        protected string Password { get; set; }
        
        protected string Error { get; set; }

        protected async Task SignIn()
        {
            Error = string.Empty;
            
            try
            {
                await AuthService.SignIn(Username, Password);
                UriHelper.NavigateTo($"/");
            }
            catch (SigninException ex)
            {
                switch (ex.Type)
                {
                    case SigninErrorType.PasswordChangeRequired:
                        UriHelper.NavigateTo($"new-password/{Username}");
                        break;
                    case SigninErrorType.NotAuthorized:
                        Error = "Username o password non corretta.";
                        break;
                    case SigninErrorType.Exception:
                        Error = "Si e' verificato un errore durante il login.";
                        break;
                    default:
                        throw;
                }
            }
        }
    }
}
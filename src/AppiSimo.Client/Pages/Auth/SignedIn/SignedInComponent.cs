namespace AppiSimo.Client.Pages.Auth.SignedIn
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Shared.Services.Abstract;

    public class SignedInComponent : BlazorComponent
    {
        [Inject]
        IAuthService Auth { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected override async Task OnInitAsync()
        {
            await Auth.SignedIn();

            UriHelper.NavigateTo("/");
        }
    }
}
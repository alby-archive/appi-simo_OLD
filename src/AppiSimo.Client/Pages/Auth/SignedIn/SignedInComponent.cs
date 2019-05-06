namespace AppiSimo.Client.Pages.Auth.SignedIn
{
    using System.Threading.Tasks;
    using Factories.Abstract;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Shared.Services.Abstract;

    public class SignedInComponent : BlazorComponent
    {
        [Inject]
        IAsyncFactory<IAuthService> AuthFactory { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected override async Task OnInitAsync()
        {
            await (await AuthFactory.CreateAsync()).SignedIn();

            UriHelper.NavigateTo("/");
        }
    }
}
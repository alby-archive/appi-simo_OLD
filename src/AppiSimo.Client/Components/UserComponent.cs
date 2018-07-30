namespace AppiSimo.Client.Components
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;
    using AppiSimo.Shared.Model;
    using Microsoft.AspNetCore.Blazor.Services;

    public class UserComponent : BlazorComponent
    {
        [Parameter]
        string Id { get; set; }   
        
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected User User { get; set; } = new User();
        
        protected override async Task OnInitAsync()
        {
            if (Id != null)
                User = await Http.GetJsonAsync<User>($"http://localhost:5001/api/users/{Id}");
        }

        protected async Task Submit()
        {
            const string baseUrl = "http://localhost:5001/api/users";
                        
            if (User.Id != Guid.Empty)
            {
                await Http.SendJsonAsync(HttpMethod.Put, baseUrl, User);
            }
            else
            {
                await Http.SendJsonAsync(HttpMethod.Post, baseUrl, User);
            }
            
            GoToHome();
        }

        protected async Task Delete()
        {
            await Http.DeleteAsync($"http://localhost:5001/api/users/{Id}");
            GoToHome();
        }

        void GoToHome()
        {
            UriHelper.NavigateTo("/");
        }
    }
}
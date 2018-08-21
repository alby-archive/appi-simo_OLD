namespace AppiSimo.Client.Components
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor.Components;
    using AppiSimo.Shared.Model;
    using Clients;
    using Microsoft.AspNetCore.Blazor.Services;

    public class UserComponent : BlazorComponent
    {
        [Parameter]
        string Id { get; set; }   
        
        [Inject]
        AppiSimoClient Client { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected User User { get; private set; } = new User();
        
        protected override async Task OnInitAsync()
        {
            if ((Id != null) & Guid.TryParse(Id, out var id))
                User = (await Client.Users.Where(u => u.Id == id).ToListAsync(Client._client)).FirstOrDefault();
        }

        protected async Task Submit()
        {
            await Client.Users.Save(User);   
            GoToHome();
        }

        protected async Task Delete()
        {
            await Client.Users.Remove(User.Id);
            GoToHome();
        }

        void GoToHome()
        {
            UriHelper.NavigateTo("/");
        }
    }
}
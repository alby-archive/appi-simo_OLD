namespace AppiSimo.Client.Pages.UserDetail
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Clients;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using AppiSimo.Shared.Model;

    public class UserDetailComponent : BlazorComponent
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
                User = (await Client.Users.Entities.Where(u => u.Id == id).ToListAsync(Client.Client)).Value.FirstOrDefault();
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
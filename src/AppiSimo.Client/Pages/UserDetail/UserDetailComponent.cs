namespace AppiSimo.Client.Pages.UserDetail
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using Clients;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;

    public class UserDetailComponent : BlazorComponent
    {
        [Parameter]
        string Id { get; set; }

        [Inject]
        AppiSimoClient<User> Client { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected User User { get; private set; } = new User();

        readonly EndPoint<User> _endPoint;

        protected UserDetailComponent()
        {
            _endPoint = Client.GetEndPoint();
        }
        
        protected override async Task OnInitAsync()
        {
            if ((Id != null) & Guid.TryParse(Id, out var id))
            {
                User = (await _endPoint.Entities.Where(u => u.Id == id).ToListAsync(Client.Client)).Value.FirstOrDefault();
            }
        }

        protected async Task Submit()
        {
            await _endPoint.Save(User);
            GoToHome();
        }

        protected async Task Delete()
        {
            await _endPoint.Remove(User.Id);
            GoToHome();
        }

        void GoToHome()
        {
            UriHelper.NavigateTo("/");
        }
    }
}
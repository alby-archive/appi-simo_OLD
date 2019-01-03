namespace AppiSimo.Client.Pages.UserDetail
{
    using System;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Shared.Pages.Abstract;

    public class UserDetailComponent : BaseComponent<User>
    {
        [Parameter]
        string Id { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected User User { get; private set; } = new User();

        protected override async Task OnInitAsync()
        {
            if ((Id != null) & Guid.TryParse(Id, out var id))
            {
                User = await EndPoint.Entity(id);
            }
        }

        protected async Task Submit()
        {
            await EndPoint.Save(User);
            GoToHome();
        }

        protected async Task Delete()
        {
            await EndPoint.Delete(User.Id);
            GoToHome();
        }

        void GoToHome()
        {
            UriHelper.NavigateTo("/");
        }
    }
}
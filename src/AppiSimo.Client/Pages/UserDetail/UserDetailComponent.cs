namespace AppiSimo.Client.Pages.UserDetail
{
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using EndPoints;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Microsoft.OData.Client;
    using Shared.Pages.Abstract;

    public class UserDetailDetailComponent : BaseDetailComponent<User, UserEndPoint>
    {
        [Inject]
        IUriHelper UriHelper { get; set; }
        
        protected UserDetailViewModel ViewModel { get; set; } = new UserDetailViewModel();

        protected override DataServiceQuery<User> Selector(DataServiceQuery<User> user) => user
            .Expand(u => u.Fit);

        protected override async Task OnInitAsync()
        {
            await base.OnInitAsync();
            ViewModel = new UserDetailViewModel(Entity);
        }

        protected override async Task Save()
        {
            await base.Save();
            GoToHome();
        }

        protected async Task SwitchStatus()
        {
            var id = Entity.Id;
            
            if (Entity.Enabled)
            {
                await EndPoint.Disable(id);
            }
            else
            {
                await EndPoint.Enable(id);
            }

            Entity.Enabled = !Entity.Enabled;
            StateHasChanged();
        }

        protected async Task PasswordReset()
        {
            await EndPoint.ResetPassword(Entity.Id);
        }

        void GoToHome()
        {
            UriHelper.NavigateTo("/");
        }
    }
}
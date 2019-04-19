namespace AppiSimo.Client.Pages.UserDetail
{
    using System;
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

        protected override DataServiceQuery<User> Selector(DataServiceQuery<User> user) => user
            .Expand(u => u.Address)
            .Expand(u => u.Fit);

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

        protected string Birthday
        {
            get => Entity.Birthday == DateTime.MinValue ? string.Empty : $"{Entity.Birthday:d}";
            set
            {
                var isValid = DateTime.TryParse(value, out var birthDate);
                if (isValid)
                {
                    Entity.Birthday = birthDate;
                }
            }
        }

        void GoToHome()
        {
            UriHelper.NavigateTo("/");
        }
    }
}
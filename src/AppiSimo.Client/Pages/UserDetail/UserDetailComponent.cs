namespace AppiSimo.Client.Pages.UserDetail
{
    using System.Linq;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.AspNetCore.Blazor.Services;
    using Microsoft.OData.Client;
    using Shared.Pages.Abstract;

    public class UserDetailDetailComponent : BaseDetailComponent<User>
    {
        [Inject]
        IUriHelper UriHelper { get; set; }

        protected override DataServiceQuery<User> Selector(DataServiceQuery<User> user) => user;

        protected override async Task Save()
        {
            await base.Save();
            GoToHome();
        }

        protected override async Task Delete()
        {
            await base.Delete();
            GoToHome();
        }

        void GoToHome()
        {
            UriHelper.NavigateTo("/");
        }
    }
}
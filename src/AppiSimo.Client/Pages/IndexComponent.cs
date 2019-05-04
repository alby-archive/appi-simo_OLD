namespace AppiSimo.Client.Pages
{
    using System.Linq;
    using System.Threading.Tasks;
    using AppiSimo.Shared.Model;
    using EndPoints;
    using Microsoft.AspNetCore.Blazor.Components;
    using Microsoft.OData.Client;
    using Shared.Pages.Abstract;
    using Shared.Pages.Searcher;
    using Shared.Services.Abstract;

    public class IndexComponent : BaseFilterComponent<User, UserEndPoint>
    {
        [Inject]
        IAuthService Auth { get; set; }

        protected override Task OnInitAsync() => Auth.TryLoadUser();

        // TODO: Move Where in base component
        protected override IQueryable<User> Selector(DataServiceQuery<User> users, Searcher searcher) => users
            .Expand(user => user.UsersEvents)
            .Expand(user => user.Address)
            .Expand(user => user.Fit)
            .Where(user => user.Surname.ToUpper().Contains(searcher.Filter.ToUpper()))
            .OrderBy(user => user.Name);
    }
}
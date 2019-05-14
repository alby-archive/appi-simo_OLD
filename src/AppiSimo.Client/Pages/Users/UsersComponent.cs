namespace AppiSimo.Client.Pages.Users
{
    using System.Linq;
    using AppiSimo.Shared.Model;
    using EndPoints;
    using Microsoft.OData.Client;
    using Shared.Pages.Abstract;
    using Shared.Pages.Searcher;

    public class UsersComponent : BaseFilterComponent<User, UserEndPoint>
    { 
        protected override IQueryable<User> Selector(DataServiceQuery<User> users, Searcher searcher) => users
            .Expand(user => user.UsersEvents)
            .Expand(user => user.Fit)
            .Expand(user => user.Profile)
            .Where(user => user.Profile.FamilyName.ToUpper().Contains(searcher.Filter.ToUpper()))
            .OrderBy(user => user.Profile.FamilyName);
    }
}
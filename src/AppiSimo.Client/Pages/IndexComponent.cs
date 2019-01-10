namespace AppiSimo.Client.Pages
{
    using System.Linq;
    using AppiSimo.Shared.Model;
    using Microsoft.OData.Client;
    using Shared.Pages.Abstract;
    using Shared.Pages.Searcher;

    public class IndexComponent : BaseDetailFilterComponent<User>
    {
        // TODO: Move Where in base component
        protected override IQueryable<User> Selector(DataServiceQuery<User> users, Searcher searcher) => users
            .Where(user => user.Surname.ToUpper().Contains(searcher.Filter.ToUpper()))
            .OrderBy(user => user.Name);
    }
}
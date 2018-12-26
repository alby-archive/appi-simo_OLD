namespace AppiSimo.Client.Pages
{
    using System.Linq;
    using AppiSimo.Shared.Model;

    public class IndexComponent : BaseFilterComponent<User>
    {
        protected override IQueryable<User> Selector(IQueryable<User> users) => users
            .Where(user => user.Surname.Contains(SearcherService.Value.Filter))
            .OrderBy(user => user.Name);
    }
}
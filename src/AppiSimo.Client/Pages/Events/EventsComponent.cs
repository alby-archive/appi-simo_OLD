namespace AppiSimo.Client.Pages.Events
{
    using System.Linq;
    using AppiSimo.Shared.Model;
    using Microsoft.OData.Client;
    using Shared.Pages.Abstract;
    using Shared.Pages.Searcher;

    public class EventsComponent : BaseFilterComponent<Event>
    {
        protected override IQueryable<Event> Selector(DataServiceQuery<Event> events, Searcher searcher) => events
            .Expand(userEvent => userEvent.Court)
            .Expand("UsersEvents($expand=User)")
            .Expand("UsersEvents($expand=Event)")
            .Where(@event => @event.UsersEvents.Any(userEvent => userEvent.User.Surname.ToUpper().Contains(searcher.Filter.ToUpper())))
            .OrderByDescending(userEvent => userEvent.StartDate);
    }
}
namespace AppiSimo.Client.Pages.Events
{
    using System.Linq;
    using AppiSimo.Shared.Model;
    using Microsoft.OData.Client;
    using Shared.Pages.Abstract;
    using Shared.Pages.Searcher;

    public class EventsComponent : BaseDetailFilterComponent<Event>
    {       
        protected override IQueryable<Event> Selector(DataServiceQuery<Event> events, Searcher _) 
            => events.Expand(e => e.Court).Expand(e => e.Heat).Expand(e => e.Light);
    }
}
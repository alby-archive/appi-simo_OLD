namespace AppiSimo.Client.Pages.Events
{
    using System.Linq;
    using AppiSimo.Shared.Model;

    public class EventsComponent : BaseFilterComponent<Event>
    {
        protected override IQueryable<Event> Selector(IQueryable<Event> @event) => @event;
    }
}
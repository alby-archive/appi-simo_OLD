namespace AppiSimo.Api.Controllers
{
    using Data;
    using Shared.Model;

    public class EventsController : EntityController<Event>
    {
        public EventsController(KingRogerContext context)
            : base(context)
        {
        }
    }
}
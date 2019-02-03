namespace AppiSimo.Api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.ContextExtensions;
    using Microsoft.AspNetCore.Mvc;
    using Shared.Model;

    public class EventsController : EntityController<Event>
    {
        public EventsController(KingRogerContext context)
            : base(context)
        {
        }

        public override async Task<IActionResult> Put(Event entity)
        {
            foreach (var usersEvent in entity.UsersEvents)
            {
                usersEvent.User = null;
            }
        
            await Context.TryUpdateManyToMany(Context.UserEvents.Where(ue => ue.EventId == entity.Id), entity.UsersEvents, e => (e.UserId, e.Cost, e.Paid));

            entity.UsersEvents = null;

            return await base.Put(entity);
        }
        
        public override async Task<IActionResult> Post(Event entity)
        {
            foreach (var usersEvent in entity.UsersEvents)
            {
                usersEvent.User = null;
            }
        
            return await base.Post(entity);
        }
    }
}
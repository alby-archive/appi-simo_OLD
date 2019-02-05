namespace AppiSimo.Api.Controllers
{
    using Data;
    using Shared.Model;

    public class UserEventController : EntityController<UserEvent>
    {
        public UserEventController(KingRogerContext context)
            : base(context)
        {
        }
    }
}
namespace AppiSimo.Api.Controllers
{
    using Data;
    using Shared.Model;

    public class UsersController : EntityController<User>
    {
        public UsersController(KingRogerContext context) : base(context)
        {
        }
    }
}

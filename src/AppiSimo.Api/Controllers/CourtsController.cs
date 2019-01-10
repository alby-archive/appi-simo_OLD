namespace AppiSimo.Api.Controllers
{
    using Data;
    using Shared.Model;

    public class CourtsController : EntityController<Court>
    {
        public CourtsController(KingRogerContext context)
            : base(context)
        {
        }
    }
}
namespace AppiSimo.Api.Controllers
{
    using Data;
    using Shared.Model;

    public class LightsController:EntityController<Light>
    {
        public LightsController(KingRogerContext context)
            : base(context)
        {
        }
    }
}
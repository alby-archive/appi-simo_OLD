namespace AppiSimo.Api.Controllers
{
    using Data;
    using Shared.Model;

    public class HeatsController:EntityController<Heat>
    {
        public HeatsController(KingRogerContext context)
            : base(context)
        {
        }
    }
}
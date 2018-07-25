namespace AppiSimo.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public abstract class DefaultController : ControllerBase
    {
    }
}
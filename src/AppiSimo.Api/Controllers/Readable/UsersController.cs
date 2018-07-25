namespace AppiSimo.Api.Controllers.Readable
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Shared.Model;

    public class UsersController : EntityController<User>
    {
        public UsersController(KingRogerContext context) : base(context)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get() => await context.Users.ToListAsync();
    }
}
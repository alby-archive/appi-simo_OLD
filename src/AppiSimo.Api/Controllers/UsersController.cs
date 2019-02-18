namespace AppiSimo.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Shared.Model;

    public class UsersController : EntityController<User>
    {
        public UsersController(KingRogerContext context)
            : base(context)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GiveMeBackMyMoney(Guid key) =>
            Ok(await Context.Set<UserEvent>().Where(userEvent => userEvent.UserId == key && !userEvent.Paid).SumAsync(payment => payment.Cost));
    }
}
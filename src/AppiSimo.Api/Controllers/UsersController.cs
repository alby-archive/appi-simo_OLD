namespace AppiSimo.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Areas.Authentication.Abstract;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Shared.Environment;
    using Shared.Model;

    public class UsersController : EntityController<User>
    {
        readonly IUserProvider _provider;

        public UsersController(KingRogerContext context, IUserProvider provider)
            : base(context)
        {
            _provider = provider;
        }

        public override async Task<IActionResult> Post(User user)
        {
            user.Enabled = false;

            if (!Env.IsDebug)
            {
                try
                {
                    await _provider.CreateAsync(user);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            await base.Post(user);

            return Ok(user);
        }

        public override async Task<IActionResult> Put(User user)
        {
            if (!Env.IsDebug)
            {
                try
                {
                    await _provider.AdminUpdateUserAttributesAsync(user);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            await base.Put(user);

            return Ok(user);
        }

        public override Task<IActionResult> Delete(Guid key) => throw new NotSupportedException();

        [HttpPost]
        public async Task<IActionResult> Enable(Guid key)
        {
            var user = await Context.Users.FindAsync(key);
            user.Enabled = true;

            if (!Env.IsDebug)
            {
                await _provider.EnableUserAsync(user.Username);
            }

            Context.Users.Update(user);

            await Context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Disable(Guid key)
        {
            var user = await Context.Users.FindAsync(key);
            user.Enabled = false;

            if (!Env.IsDebug)
            {
                await _provider.DisableUserAsync(user.Username);
            }

            Context.Users.Update(user);

            await Context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(Guid key)
        {
            if (Env.IsDebug)
            {
                return Ok();
            }

            var user = await Context.Users.FindAsync(key);

            await _provider.AdminResetUserPassword(user.Username);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GiveMeBackMyMoney(Guid key) =>
            Ok(await Context.Set<UserEvent>().Where(userEvent => userEvent.UserId == key && !userEvent.Paid).SumAsync(payment => payment.Cost));
    }
}
namespace AppiSimo.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Areas.Authentication.Abstract;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
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

            try
            {
                await _provider.CreateAsync(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            await base.Post(user);

            return Ok(user);
        }

        public override async Task<IActionResult> Put(User user)
        {
            try
            {
                await _provider.AdminUpdateUserAttributesAsync(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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

            try
            {
                await _provider.EnableUserAsync(user.Profile.Sub);
            }
            catch (Exception e)
            {
                return BadRequest(e);
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

            try
            {
                await _provider.DisableUserAsync(user.Profile.Sub);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            Context.Users.Update(user);
            await Context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(Guid key)
        {
            var user = await Context.Users.FindAsync(key);

            try
            {
                await _provider.AdminResetUserPassword(user.Profile.Sub);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GiveMeBackMyMoney(Guid key) =>
            Ok(await Context.Set<UserEvent>().Where(userEvent => userEvent.UserId == key && !userEvent.Paid).SumAsync(payment => payment.Cost));
    }
}
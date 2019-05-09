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
            Guid sub;

            try
            {
                sub = await _provider.CreateAsync(user.Profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            user.Enabled = false;
            user.Profile.Sub = sub;
            
            await base.Post(user);

            try
            {
                await DisableAsync(user.Id);
            }
            catch (Exception e)
            {
                user.Enabled = true;
                await UpdateAsync(user);

                return BadRequest($"User created! But remind enabled. Exception: {e.Message}");
            }
            // TODO: Genders and Roles in DB
            return Ok(user);
        }

        public override async Task<IActionResult> Put(User user)
        {
            try
            {
                await _provider.AdminUpdateUserAttributesAsync(user.Profile);
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
            try
            {
                await EnableAsync(key);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Disable(Guid key)
        {
            try
            {
                await DisableAsync(key);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(Guid key)
        {
            var user = await _context.Users.FindAsync(key);

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
            Ok(await _context.Set<UserEvent>().Where(userEvent => userEvent.UserId == key && !userEvent.Paid).SumAsync(payment => payment.Cost));

        async Task EnableAsync(Guid key) => await EnableDisableAsync(key, async sub => await _provider.EnableUserAsync(sub));

        async Task DisableAsync(Guid key) => await EnableDisableAsync(key, async sub => await _provider.DisableUserAsync(sub));

        async Task EnableDisableAsync(Guid key, Func<Guid, Task> selector)
        {
            var user = await _context.Users.FindAsync(key);

            await selector(user.Profile.Sub);

            user.Enabled = !user.Enabled;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
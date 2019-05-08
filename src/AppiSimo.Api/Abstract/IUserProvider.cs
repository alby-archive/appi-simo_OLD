namespace AppiSimo.Api.Areas.Authentication.Abstract
{
    using System;
    using System.Threading.Tasks;
    using Shared.Model;

    public interface IUserProvider
    {
        Task CreateAsync(User user);
        Task AdminUpdateUserAttributesAsync(User user);
        Task DisableUserAsync(Guid sub);
        Task EnableUserAsync(Guid sub);

        Task AdminResetUserPassword (Guid sub);
    }
}
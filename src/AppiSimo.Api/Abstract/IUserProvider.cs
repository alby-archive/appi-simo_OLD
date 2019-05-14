namespace AppiSimo.Api.Areas.Authentication.Abstract
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Shared.Model;

    public interface IUserProvider
    {
        Task<Guid> CreateAsync(Profile profile);
        Task<HttpStatusCode> AdminUpdateUserAttributesAsync(Profile profile);
        Task DisableUserAsync(Guid key);
        Task EnableUserAsync(Guid key);

        Task AdminResetUserPassword (Guid key);
    }
}
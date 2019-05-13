namespace AppiSimo.Api.Providers
{
    using System;
    using System.Threading.Tasks;
    using Areas.Authentication.Abstract;
    using Shared.Model;

    public class CognitoUserProviderDisconnected : IUserProvider
    {
        public Task<Guid> CreateAsync(Profile profile) => Task.FromResult(Guid.Empty);

        public Task AdminUpdateUserAttributesAsync(Profile profile) => Task.CompletedTask;

        public Task DisableUserAsync(Guid key) => Task.CompletedTask;

        public Task EnableUserAsync(Guid key) => Task.CompletedTask;

        public Task AdminResetUserPassword(Guid key) => Task.CompletedTask;
    }
}
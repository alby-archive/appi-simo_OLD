namespace AppiSimo.Api.Providers
{
    using System;
    using System.Threading.Tasks;
    using Areas.Authentication.Abstract;
    using Shared.Model;

    public class CognitoUserProviderDisconnected : IUserProvider
    {
        public Task CreateAsync(User user) => (Task<string>) Task.CompletedTask;

        public Task AdminUpdateUserAttributesAsync(User user) => Task.CompletedTask;

        public Task DisableUserAsync(Guid username) => Task.CompletedTask;

        public Task EnableUserAsync(Guid username) => Task.CompletedTask;

        public Task AdminResetUserPassword(Guid username) => Task.CompletedTask;
    }
}
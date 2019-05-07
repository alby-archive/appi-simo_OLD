namespace AppiSimo.Api.Providers
{
    using System.Threading.Tasks;
    using Areas.Authentication.Abstract;
    using Shared.Model;

    public class CognitoUserProviderDisconnected : IUserProvider
    {
        public Task CreateAsync(User user) => Task.CompletedTask;

        public Task AdminUpdateUserAttributesAsync(User user) => Task.CompletedTask;

        public Task DisableUserAsync(string username) => Task.CompletedTask;

        public Task EnableUserAsync(string username) => Task.CompletedTask;

        public Task AdminResetUserPassword(string username) => Task.CompletedTask;
    }
}
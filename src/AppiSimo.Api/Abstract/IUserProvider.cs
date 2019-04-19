namespace AppiSimo.Api.Areas.Authentication.Abstract
{
    using System.Threading.Tasks;
    using Shared.Model;

    public interface IUserProvider
    {
        Task CreateAsync(User user);
        Task AdminUpdateUserAttributesAsync(User user);
        Task DisableUserAsync(string username);
        Task EnableUserAsync(string username);

        Task AdminResetUserPassword (string username);
    }
}
namespace AppiSimo.Api.Providers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Amazon.CognitoIdentityProvider;
    using Amazon.CognitoIdentityProvider.Model;
    using Areas.Authentication.Abstract;
    using Environment;
    using Shared.Model;

    public class CognitoUserProvider : IUserProvider, IDisposable
    {
        readonly Cognito _cognito;
        readonly AmazonCognitoIdentityProviderClient _provider;

        public CognitoUserProvider(
            Cognito cognito,
            AmazonCognitoIdentityProviderClient provider)
        {
            _cognito = cognito;
            _provider = provider;
        }

        public async Task<Guid> CreateAsync(Profile profile)
        {
            var request = CreateUser(profile);

            AdminCreateUserResponse result = null;

            try
            {
                result = await _provider.AdminCreateUserAsync(request);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }

            return Guid.Parse(result.User.Attributes.FirstOrDefault(a => a.Name == "sub").Value);
        }

        public async Task AdminUpdateUserAttributesAsync(Profile profile)
        {
            var request = new AdminUpdateUserAttributesRequest
            {
                UserPoolId = _cognito.UserPool.Id,
                Username = $"{profile.Sub}",
                UserAttributes = profile.GetCognitoAttributes().ToList()
            };

            try
            {
                await _provider.AdminUpdateUserAttributesAsync(request);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e}");
            }
        }

        public async Task DisableUserAsync(Guid key)
        {
            var request = new AdminDisableUserRequest
            {
                Username = $"{key}",
                UserPoolId = _cognito.UserPool.Id
            };

            await _provider.AdminDisableUserAsync(request);
        }

        public async Task EnableUserAsync(Guid key)
        {
            var request = new AdminEnableUserRequest
            {
                Username = $"{key}",
                UserPoolId = _cognito.UserPool.Id
            };

            await _provider.AdminEnableUserAsync(request);
        }

        public async Task AdminResetUserPassword(Guid key)
        {
            var request = new AdminResetUserPasswordRequest
            {
                Username = $"{key}",
                UserPoolId = _cognito.UserPool.Id
            };

            await _provider.AdminResetUserPasswordAsync(request);
        }

        AdminCreateUserRequest CreateUser(Profile profile)
        {
            var password = $"RSC-{Guid.NewGuid()}";
            Console.WriteLine($@"PASSWORD: {password}");

            return new AdminCreateUserRequest
            {
                UserPoolId = _cognito.UserPool.Id,
                Username = profile.Email,
                TemporaryPassword = password,
                MessageAction = MessageActionType.SUPPRESS,
                UserAttributes = profile.GetCognitoAttributes().ToList(),
            };
        }

        public void Dispose()
        {
            _provider.Dispose();
        }
    }
}
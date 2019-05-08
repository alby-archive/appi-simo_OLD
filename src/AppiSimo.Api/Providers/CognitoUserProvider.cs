namespace AppiSimo.Api.Providers
{
    using System;
    using System.Collections.Generic;
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

        public async Task CreateAsync(User user)
        {
            var request = CreateUser(user);

            AdminCreateUserResponse result = null;

            try
            {
                result = await _provider.AdminCreateUserAsync(request);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }

            user.Profile.Sub = Guid.Parse(result.User.Attributes.FirstOrDefault(a => a.Name == "sub").Value);
        }

        public async Task AdminUpdateUserAttributesAsync(User user)
        {
            var request = new AdminUpdateUserAttributesRequest
            {
                UserPoolId = _cognito.UserPool.Id,
                Username = $"{user.Profile.Sub}",
                UserAttributes = GetUserAttributes(user)
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

        public async Task DisableUserAsync(Guid username)
        {
            var request = new AdminDisableUserRequest
            {
                Username = $"{username}",
                UserPoolId = _cognito.UserPool.Id
            };

            await _provider.AdminDisableUserAsync(request);
        }

        public async Task EnableUserAsync(Guid username)
        {
            var request = new AdminEnableUserRequest
            {
                Username = $"{username}",
                UserPoolId = _cognito.UserPool.Id
            };

            await _provider.AdminEnableUserAsync(request);
        }

        public async Task AdminResetUserPassword(Guid username)
        {
            var request = new AdminResetUserPasswordRequest
            {
                Username = $"{username}",
                UserPoolId = _cognito.UserPool.Id
            };

            await _provider.AdminResetUserPasswordAsync(request);
        }

        AdminCreateUserRequest CreateUser(User user) => new AdminCreateUserRequest
        {
            UserPoolId = _cognito.UserPool.Id,
            Username = user.Profile.Email,
            TemporaryPassword = $"RSC-{Guid.NewGuid()}",
            MessageAction = MessageActionType.SUPPRESS,
            UserAttributes = GetUserAttributes(user)
        };

        static List<AttributeType> GetUserAttributes(User user) =>
            new List<AttributeType>
            {
                new AttributeType
                {
                    Value = user.Profile.FamilyName,
                    Name = "family_name"
                },
                new AttributeType
                {
                    Value = user.Profile.GivenName,
                    Name = "given_name"
                },
                new AttributeType
                {
                    Value = $"{user.Profile.Birthdate:d}",
                    Name = "birthdate"
                },
                new AttributeType
                {
                    Value = user.Profile.Address,
                    Name = "address"
                },
                new AttributeType
                {
                    Value = user.Profile.Email,
                    Name = "email"
                },
                new AttributeType
                {
                    Value = user.Profile.Email != null ? "true" : "false",
                    Name = "email_verified"
                }
            };

        public void Dispose()
        {
            _provider.Dispose();
        }
    }
}
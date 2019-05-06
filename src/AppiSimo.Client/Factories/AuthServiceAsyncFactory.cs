namespace AppiSimo.Client.Factories
{
    using System.Threading.Tasks;
    using Abstract;
    using Environment;
    using Shared.Services;
    using Shared.Services.Abstract;

    public class AuthServiceAsyncFactory : IAsyncFactory<IAuthService>
    {
        readonly AuthService _auth;

        public AuthServiceAsyncFactory(CognitoClient config)
        {
            _auth = new AuthService(config);
        }

        public async Task<IAuthService> CreateAsync()
        {
            await _auth.TryLoadUser();

            return _auth;
        }
    }
}
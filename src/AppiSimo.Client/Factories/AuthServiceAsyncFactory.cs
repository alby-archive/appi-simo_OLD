namespace AppiSimo.Client.Factories
{
    using System.Threading.Tasks;
    using Abstract;
    using AppiSimo.Shared.JSonConverters;
    using Environment;
    using Providers;
    using Shared.Model;
    using Shared.Services;
    using Shared.Services.Abstract;

    public class AuthServiceAsyncFactory : IAsyncFactory<IAuthService>
    {
        readonly AuthService _auth;

        public AuthServiceAsyncFactory(CognitoClient config, IContractProvider<RootObject, CognitoContractResolver> provider)
        {
            _auth = new AuthService(config, provider);
        }

        public async Task<IAuthService> CreateAsync()
        {
            await _auth.TryLoadUser();

            return _auth;
        }
    }
}
namespace AppiSimo.Client.Shared.Services
{
    using System.Reactive.Subjects;
    using System.Threading.Tasks;
    using Abstract;
    using AppiSimo.Shared.JSonConverters;
    using Environment;
    using Microsoft.JSInterop;
    using Model;
    using Newtonsoft.Json;
    using Providers;

    public class AuthService : IAuthService
    {
        static IJSInProcessRuntime Js => (IJSInProcessRuntime) JSRuntime.Current;

        public BehaviorSubject<RootObject> User { get; } = new BehaviorSubject<RootObject>(value: null);
        public RootObject CurrentUser => User.Value;

        readonly CognitoClient _config;
        readonly IContractProvider<RootObject, CognitoContractResolver> _provider;

        public AuthService(CognitoClient config, IContractProvider<RootObject, CognitoContractResolver> provider)
        {
            _config = config;
            _provider = provider;
        }

        public async Task TryLoadUser() =>
            SetSubject(await Js.InvokeAsync<string>(
                "interop.authentication.tryLoadUser",
                JsonConvert.SerializeObject(_config)
            ));

        public async Task SignIn() =>
            await Js.InvokeAsync<Task>(
                "interop.authentication.signIn",
                JsonConvert.SerializeObject(_config)
            );

        public async Task SignedIn()
        {
            SetSubject(await Js.InvokeAsync<string>(
                "interop.authentication.signedIn",
                JsonConvert.SerializeObject(_config)
            ));

            ClearSignedInHistory();
        }

        public void SignOut() =>
            Js.Invoke<object>(
                "interop.authentication.signOut",
                JsonConvert.SerializeObject(_config)
            );

        void SetSubject(string response) => User.OnNext(_provider.Normalize(response));

        static void ClearSignedInHistory() => Js.Invoke<object>("interop.authentication.clearSignedInHistory");
    }
}
namespace AppiSimo.Client.Shared.Services
{
    using System.Reactive.Subjects;
    using System.Threading.Tasks;
    using Abstract;
    using Environment;
    using Microsoft.JSInterop;
    using Model;
    using Newtonsoft.Json;

    public class AuthService : IAuthService
    {
        static IJSInProcessRuntime Js => (IJSInProcessRuntime) JSRuntime.Current;

        public BehaviorSubject<RootObject> User { get; } = new BehaviorSubject<RootObject>(value: null);
        public RootObject CurrentUser => User.Value;

        readonly CognitoClient _config;

        public AuthService(CognitoClient config)
        {
            _config = config;
        }

        public async Task TryLoadUser() =>
            SetSubject(await Js.InvokeAsync<string>(
                "interop.authentication.tryLoadUser",
                JsonConvert.SerializeObject(_config)
            ));

        public async Task SignIn() =>
            // TODO: Invoke<void>
            await Js.InvokeAsync<Task>(
                "interop.authentication.signIn",
                JsonConvert.SerializeObject(_config)
            );

        public async Task SignedIn() =>
            SetSubject(await Js.InvokeAsync<string>(
                "interop.authentication.signedIn",
                JsonConvert.SerializeObject(_config)
            ));

        public void SignOut() =>
            // TODO: Invoke<void>
            Js.Invoke<object>(
                "interop.authentication.signOut",
                JsonConvert.SerializeObject(_config)
            );

        void SetSubject(string response) => User.OnNext(JsonConvert.DeserializeObject<RootObject>(response));
    }
}
namespace AppiSimo.Client.Shared.Services
{
    using System;
    using System.Reactive.Subjects;
    using System.Threading.Tasks;
    using Environment;
    using Microsoft.JSInterop;

    public class AuthService
    { 
        static IJSInProcessRuntime Js => (IJSInProcessRuntime) JSRuntime.Current;

        public BehaviorSubject<RootObject> User { get; } = new BehaviorSubject<RootObject>(value: null);
        public RootObject CurrentUser => User.Value;
        public bool IsLogged => CurrentUser != null;
        
        readonly AuthConfig _config;
        
        public AuthService(AuthConfig config)
        {
            _config = config;
        }

        public async Task TryLoadUser() => SetSubject(await Js.InvokeAsync<Result<RootObject>>("interop.authentication.tryLoadUser"));

        public async Task SignIn() => SetSubject(await Js.InvokeAsync<Result<RootObject>>("interop.authentication.signIn"));
        
        public async Task SignedIn() => SetSubject(await Js.InvokeAsync<Result<RootObject>>("interop.authentication.signedIn"));

        public void SignOut()
        {
            Js.Invoke<Result<RootObject>>("interop.authentication.signOut");
            
            SetSubject(response: null);
        }

        void SetSubject(Result<RootObject> response)
        {
            if (response == null || !response.IsValid)
            {
                throw new Exception(response.Error);
            }

            User.OnNext(response.Value);
        }
    }
    
    public class Profile
    {
        public string sub { get; set; }
        public string email_verified { get; set; }
        public string token_use { get; set; }
        public int auth_time { get; set; }
        public string email { get; set; }
        public string username { get; set; }
    }

    public class RootObject
    {
        public string id_token { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public Profile profile { get; set; }
        public int expires_at { get; set; }
        public string state { get; set; }
    }
        
    public class Result<T>
    {
        public T Value { get; set; }
        public string Error { get; set; }
        public bool IsValid => Error == null;
    }
}
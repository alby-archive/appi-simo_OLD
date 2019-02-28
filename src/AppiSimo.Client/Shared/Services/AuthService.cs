namespace AppiSimo.Client.Shared.Services
{
    using System;
    using System.Threading.Tasks;
    using Environment;
    using Microsoft.AspNetCore.Blazor.Services;
    using Microsoft.JSInterop;
    using Model;

    public class AuthService
    {
        readonly AuthConfig _config;

        public AuthService(AuthConfig config)
        {
            _config = config;
        }

        static IJSInProcessRuntime Js => (IJSInProcessRuntime) JSRuntime.Current;

        public bool IsLoggedIn() =>
            Js.Invoke<bool>("interop.authentication.isLoggedIn", _config);

        public Task<string> SignIn(AuthUser authUser) => 
            Js.InvokeAsync<string>("interop.authentication.signIn", authUser.Username, authUser.Password, _config);

        public Task ChangePassword(Password password)
        {
            return null;
        }
        
        public void SignOut() =>
            Js.Invoke<string>("interop.authentication.logout", _config);

//        public async Task<AuthenticatedUser> TryGetUserAsync()
//        {
//            var result = await Js.InvokeAsync<GetTokenResult>("interop.authentication.getTokenAsync", _config);
//            if (result == null)
//            {
//                return null;
//            }
//
//            return new AuthenticatedUser(result.UserName, new Token(result.IdToken, result.Expires));
//
//        }

        class GetTokenResult
        {
            public string IdToken { get; set; }

            public DateTime Expires { get; set; }

            public string UserName { get; set; }
        }
    }
}
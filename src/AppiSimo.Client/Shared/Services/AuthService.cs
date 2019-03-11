namespace AppiSimo.Client.Shared.Services
{
    using System;
    using System.Reactive.Subjects;
    using System.Threading.Tasks;
    using Environment;
    using Microsoft.JSInterop;
    using Model;

    public class AuthService
    {
        public BehaviorSubject<AuthUser> User { get; } = new BehaviorSubject<AuthUser>(value: null);
        readonly AuthConfig _config;

        public AuthService(AuthConfig config)
        {
            _config = config;
        }

        void NextUser(AuthUser authUser)
        {
            User.OnNext(authUser);
        }

        static IJSInProcessRuntime Js => (IJSInProcessRuntime) JSRuntime.Current;

        public async Task SignIn(string username, string password)
        {
            var result = await Js.InvokeAsync<Result<Token>>("interop.authentication.signIn", username, password, _config);

            if (result.Error != null)
            {
                throw new SigninException(result.Error.Message, result.Error.Type);
            }

            NextUser(new AuthUser(username, result.Value));
        }

        public void SignOut()
        {
            if (User.Value == null)
            {
                return;
            }

            Js.Invoke<Result<Token>>("interop.authentication.signOut", User.Value.Username, _config);

            NextUser(authUser: null);
        }

        public Task ChangePassword(string username, string oldPassword, string newPassword) => Js.InvokeAsync<Task>("interop.authentication.completeNewPasswordChallenge", username, oldPassword, newPassword, _config);

        class SigninError
        {
            public SigninErrorType Type { get; private set; }
            public string Message { get; private set; }
        }

        class Result<T>
        {
            public T Value { get; private set; }
            public SigninError Error { get; private set; }
        }
    }

    public enum SigninErrorType
    {
        Exception,
        PasswordChangeRequired,
        NotAuthorized
    }

    class SigninException : Exception
    {
        public SigninException(string message, SigninErrorType type)
            : base(message)
        {
            Type = type;
        }

        public SigninErrorType Type { get; }
    }
}
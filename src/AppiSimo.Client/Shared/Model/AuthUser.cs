namespace AppiSimo.Client.Shared.Model
{
    using Services;

    public class AuthUser
    {
        public AuthUser(string username, Token token)
        {
            Username = username;
            Token = token;
        }

        public string Username { get;  }
        public Token Token { get;  }
    }
    
    public class Token
    {
        public string Value { get; private set; }
    }
}
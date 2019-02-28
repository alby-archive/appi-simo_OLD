namespace AppiSimo.Client.Environment
{
    using System.Collections.Generic;

    public class AuthConfig
    {
        public string ClientId { get; set; }
        public string UserPoolId { get; set;}
        public IReadOnlyList<string> Scopes { get; set; }
        public string RedirectUri { get; }
        public string PostLogoutRedirectUri { get; set; }
    }
}
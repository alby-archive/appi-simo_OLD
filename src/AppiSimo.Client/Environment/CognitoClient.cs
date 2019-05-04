namespace AppiSimo.Client.Environment
{
    using Newtonsoft.Json;

    public class CognitoClient
    {
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }

        [JsonProperty(PropertyName = "authority")]
        public string Authority { get; set; }

        [JsonProperty(PropertyName = "automaticSilentRenew")]
        public bool AutomaticSilentRenew { get; set; }

        [JsonProperty(PropertyName = "redirect_uri")]
        public string RedirectUri { get; set; }

        [JsonProperty(PropertyName = "post_logout_redirect_uri")]
        public string PostLogoutRedirectUri { get; set; }

        [JsonProperty(PropertyName = "response_type")]
        public string ResponseType { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        [JsonProperty(PropertyName = "filterProtocolClaims")]
        public bool FilterProtocolClaims { get; set; }

        [JsonProperty(PropertyName = "loadUserInfo")]
        public bool LoadUserInfo { get; set; }

        [JsonProperty(PropertyName = "prompt")]
        public string Prompt { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public Metadata Metadata { get; set; }

        // valorize by auth.js. Hero only for visibility
        [JsonIgnore]
        public string UserStore { get; set; }
    }
}
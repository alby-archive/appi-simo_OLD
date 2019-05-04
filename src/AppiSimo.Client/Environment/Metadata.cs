namespace AppiSimo.Client.Environment
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Metadata
    {
        [JsonProperty(PropertyName = "authorization_endpoint")]
        public string AuthorizationEndpoint { get; set; }

        [JsonProperty(PropertyName = "id_token_signing_alg_values_supported")]
        public IEnumerable<string> IdTokenSigningAlgValuesSupported { get; set; }

        [JsonProperty(PropertyName = "issuer")]
        public string Issuer { get; set; }

        [JsonProperty(PropertyName = "jwks_uri")]
        public string JwksUri { get; set; }

        [JsonProperty(PropertyName = "response_types_supported")]
        public IEnumerable<string> ResponseTypesSupported { get; set; }

        [JsonProperty(PropertyName = "scopes_supported")]
        public IEnumerable<string> ScopesSupported { get; set; }

        [JsonProperty(PropertyName = "subject_types_supported")]
        public IEnumerable<string> SubjectTypesSupported { get; set; }

        [JsonProperty(PropertyName = "token_endpoint")]
        public string TokenEndpoint { get; set; }

        [JsonProperty(PropertyName = "token_endpoint_auth_methods_supported")]
        public IEnumerable<string> TokenEndpointAuthMethodsSupported { get; set; }

        [JsonProperty(PropertyName = "userinfo_endpoint")]
        public string UserInfoEndpoint { get; set; }

        [JsonProperty(PropertyName = "end_session_endpoint")]
        public string EndSessionEndpoint { get; set; }
    }
}
namespace AppiSimo.Client.Shared.Model
{
    using System;
    using Newtonsoft.Json;

    public class Profile
    {
        [JsonProperty(PropertyName = "sub")]
        public Guid Sub { get; set; }

        [JsonProperty(PropertyName = "email_verified")]
        public bool EmailVerified { get; set; }

        [JsonProperty(PropertyName = "token_use")]
        public string TokenUse { get; set; }

        [JsonProperty(PropertyName = "cognito:username")]
        public string CognitoUsername { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
    }
}
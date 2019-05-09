namespace AppiSimo.Shared.Model
{
    using System;
    using Newtonsoft.Json;

    public class MandatoryProfile : Entity
    {
        [JsonProperty(PropertyName = "sub")]
        public Guid Sub { get; set; }

        [JsonProperty(PropertyName = "given_name")]
        public string GivenName { get; set; }

        [JsonProperty(PropertyName = "family_name")]
        public string FamilyName { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public Genders Gender { get; set; }

        [JsonProperty(PropertyName = "birthdate")]
        public DateTime Birthdate { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "email_verified")]
        public bool EmailVerified => Email != null;

        [JsonProperty(PropertyName = "custom:role")]
        public Roles Role { get; set; } = Roles.User;
    }
}
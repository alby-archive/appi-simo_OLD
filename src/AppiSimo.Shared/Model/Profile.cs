namespace AppiSimo.Shared.Model
{
    using Newtonsoft.Json;

    public class Profile : MandatoryProfile
    {
        [JsonProperty(PropertyName = "preferred_username")]
        public string PreferredUsername { get; set; }

        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "phone_number_verified")]
        public bool PhoneNumberVerified => PhoneNumber != null;
    }
}
namespace AppiSimo.Shared.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Amazon.CognitoIdentityProvider.Model;
    using Newtonsoft.Json;

    public class Profile : MandatoryProfile
    {
        [JsonProperty(PropertyName = "preferred_username")]
        public string PreferredUsername { get; set; }

        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "phone_number_verified")]
        public bool PhoneNumberVerified => PhoneNumber != null;

        static string GetAttributeName(string name) =>
            ((JsonPropertyAttribute) Attribute.GetCustomAttribute(typeof(Profile).GetProperty(name) ?? throw new Exception("Property Not Found!"), typeof(JsonPropertyAttribute)))?.PropertyName;

        public IEnumerable<AttributeType> GetCognitoAttributes() =>
            new List<AttributeType>
            {
                new AttributeType
                {
                    Name = GetAttributeName(nameof(PreferredUsername)),
                    Value = PreferredUsername
                },
                new AttributeType
                {
                    Name = GetAttributeName(nameof(PhoneNumber)),
                    Value = PhoneNumber
                },
                new AttributeType
                {
                    Name = GetAttributeName(nameof(PhoneNumberVerified)),
                    Value = PhoneNumberVerified.ToString().ToLower()
                },
                new AttributeType
                {
                    Name = GetAttributeName(nameof(GivenName)),
                    Value = GivenName
                },
                new AttributeType
                {
                    Name = GetAttributeName(nameof(FamilyName)),
                    Value = FamilyName
                },
                new AttributeType
                {
                    Name = GetAttributeName(nameof(Gender)),
                    Value = Gender.ToString()
                },
                new AttributeType
                {
                    Name = GetAttributeName(nameof(Birthdate)),
                    Value = Birthdate.ToString("d")
                },
                new AttributeType
                {
                    Name = GetAttributeName(nameof(Address)),
                    Value = Address
                },
                new AttributeType
                {
                    Name = GetAttributeName(nameof(Email)),
                    Value = Email
                },
                new AttributeType
                {
                    Name = GetAttributeName(nameof(EmailVerified)),
                    Value = EmailVerified.ToString().ToLower()
                },
                new AttributeType
                {
                    Name = GetAttributeName(nameof(Role)),
                    Value = Role.ToString().ToLower()
                }
            }.Where(a => !string.IsNullOrEmpty(a.Value));
    }
}
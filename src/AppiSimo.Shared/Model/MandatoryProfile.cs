namespace AppiSimo.Shared.Model
{
    using System;
    using Attributes;
    using Microsoft.OData.Client;

    [OriginalName("MandatoryProfile")]
    public class MandatoryProfile : Entity
    {
        [CognitoContract(Convention = "sub")]
        public Guid Sub { get; set; }

        [CognitoContract(Convention = "given_name")]
        public string GivenName { get; set; }

        [CognitoContract(Convention = "family_name")]
        public string FamilyName { get; set; }

        [CognitoContract(Convention = "gender")]
        public Genders Gender { get; set; }

        [CognitoContract(Convention = "birthdate")]
        public DateTime Birthdate { get; set; }

        [CognitoContract(Convention = "address")]
        public string Address { get; set; }

        [CognitoContract(Convention = "email")]
        public string Email { get; set; }

        [CognitoContract(Convention = "email_verified")]
        public bool EmailVerified => Email != null;

        [CognitoContract(Convention = "custom:role")]
        public Roles Role { get; set; } = Roles.User;
    }
}
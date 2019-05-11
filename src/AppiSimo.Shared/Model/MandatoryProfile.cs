namespace AppiSimo.Shared.Model
{
    using System;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    
//    [DataContract]
    public class MandatoryProfile : Entity
    {
//        [DataMember(Name = "sub")]
        [JsonProperty(PropertyName = "sub")]
        public Guid Sub { get; set; }

//        [DataMember(Name = "given_name")]
        [JsonProperty(PropertyName = "given_name")]
        public string GivenName { get; set; }

//        [DataMember(Name = "family_name")]
        [JsonProperty(PropertyName = "family_name")]
        public string FamilyName { get; set; }

//        [DataMember(Name = "gender")]
        [JsonProperty(PropertyName = "gender")]
        public Genders Gender { get; set; }

//        [DataMember(Name = "birthdate")]
        [JsonProperty(PropertyName = "birthdate")]
        public DateTime Birthdate { get; set; }

//        [DataMember(Name = "address")]
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
        
//        [DataMember(Name = "email")]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

//        [DataMember(Name = "email_verified")]
        [JsonProperty(PropertyName = "email_verified")]
        public bool EmailVerified => Email != null;

//        [DataMember(Name = "custom:role")]
        [JsonProperty(PropertyName = "custom:role")]
        public Roles Role { get; set; } = Roles.User;
    }
}
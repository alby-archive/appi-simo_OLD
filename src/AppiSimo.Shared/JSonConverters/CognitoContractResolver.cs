namespace AppiSimo.Shared.JSonConverters
{
    using System.Reflection;
    using Attributes;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class CognitoContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            var attribute = member.GetCustomAttribute<CognitoContract>();
            if (attribute == null)
            {
                return property;
            }

            property.PropertyName = attribute.Convention;
            property.UnderlyingName = member.Name;

            return property;
        }
    }
}
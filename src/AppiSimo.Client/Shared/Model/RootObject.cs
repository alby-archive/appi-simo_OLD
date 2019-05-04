namespace AppiSimo.Client.Shared.Model
{
    using Newtonsoft.Json;

    public class RootObject
    {
        [JsonProperty(PropertyName = "id_token")]
        public string IdToken { get; set; }

        [JsonProperty(PropertyName = "profile")]
        public Profile Profile { get; set; }
    }
}
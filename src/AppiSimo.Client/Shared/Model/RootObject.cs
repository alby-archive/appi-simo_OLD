namespace AppiSimo.Client.Shared.Model
{
    using AppiSimo.Shared.Model;
    using Newtonsoft.Json;

    public class RootObject
    {
        [JsonProperty(PropertyName = "id_token")]
        public string IdToken { get; set; }

        [JsonProperty(PropertyName = "profile")]
        public MandatoryProfile MandatoryProfile { get; set; }
    }
}
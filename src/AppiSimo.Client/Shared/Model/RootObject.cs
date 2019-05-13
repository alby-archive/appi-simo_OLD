namespace AppiSimo.Client.Shared.Model
{
    using AppiSimo.Shared.Attributes;
    using AppiSimo.Shared.Model;

    public class RootObject
    {
        [CognitoContract(Convention = "id_token")]
        public string IdToken { get; set; }

        [CognitoContract(Convention = "profile")]
        public Profile Profile { get; set; }
    }
}
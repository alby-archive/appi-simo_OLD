namespace AppiSimo.Client.Environment
{
    using AppiSimo.Shared.Environment;

    public class Configuration
    {
        public string ApiUrl { get; set; }
        public CognitoClient CognitoClient { get; set; }
    }
}
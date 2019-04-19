namespace AppiSimo.Client.Environment
{
    public class Configuration
    {
        public string ApiUrl { get; set; }
        public AuthConfig AuthConfig { get; set; }

        public static string GetSection(string aws)
        {
            throw new System.NotImplementedException();
        }
    }
}
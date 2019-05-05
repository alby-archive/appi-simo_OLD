namespace AppiSimo.Api.Environment
{
    using Amazon;

    public class Cognito
    {
        public string Region { get; set; }
        public UserPool UserPool { get; set; }
        public IdentityAccessManagement IdentityAccessManagement { get; set; }

        // In netcore 2.1 if Region stay under RegionEndPoint, it's initialized with null. Probably framework bug.  
        public RegionEndpoint RegionEndpoint => RegionEndpoint.GetBySystemName(Region);
    }
}
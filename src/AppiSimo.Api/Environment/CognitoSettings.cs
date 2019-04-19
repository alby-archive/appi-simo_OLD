namespace AppiSimo.Api.Environment
{
    using Amazon;

    public class CognitoSettings
    {
        public string Region { get; set; }
        public RegionEndpoint RegionEndpoint => RegionEndpoint.GetBySystemName(Region);
        public string UserPoolId { get; set; }
    }
}
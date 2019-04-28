namespace AppiSimo.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Shared.Environment;
    
    public class AppSettingsController : Controller
    {
        readonly Cognito _configuration;

        public AppSettingsController(Cognito configuration)
        {
            _configuration = configuration;
        }

        public string Index()
        {
            return $"Region: {_configuration.Region}; UserPoolId: {_configuration.UserPool.Id}";
        }
    }
}
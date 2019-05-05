namespace AppiSimo.Api.Controllers
{
    using Environment;
    using Microsoft.AspNetCore.Mvc;

    public class AppSettingsController : Controller
    {
        readonly Cognito _configuration;

        public AppSettingsController(Cognito configuration)
        {
            _configuration = configuration;
        }

        public string Index() => $"Region: {_configuration.Region}; UserPoolId: {_configuration.UserPool.Id}";
    }
}
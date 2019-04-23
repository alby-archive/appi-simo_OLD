namespace AppiSimo.Api.Starting
{
    using Amazon.CognitoIdentityProvider;
    using Areas.Authentication.Abstract;
    using Areas.Authentication.Providers;
    using Autofac;
    using Microsoft.Extensions.Options;
    using Shared.Environment;

    public class RepositoryHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CognitoUserProvider>().As<IUserProvider>();
            builder.Register(b =>
            {
                var config = b.Resolve<IOptionsMonitor<AmazonWebServicesConfig>>().CurrentValue;
                return new AmazonCognitoIdentityProviderClient(
                    config.IdentityAccessManagementSettings.AccessKeyId,
                    config.IdentityAccessManagementSettings.SecretAccessKey,
                    config.CognitoSettings.RegionEndpoint);
            });
        }
    }
}
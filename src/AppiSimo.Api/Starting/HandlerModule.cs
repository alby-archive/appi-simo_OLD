namespace AppiSimo.Api.Starting
{
    using Amazon.CognitoIdentityProvider;
    using Areas.Authentication.Abstract;
    using Autofac;
    using Environment;
    using Providers;

    public class HandlerModule : Module
    {
        readonly Cognito _config;

        public HandlerModule(Cognito config)
        {
            _config = config;
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CognitoUserProvider>().As<IUserProvider>();

            builder.Register(b => new AmazonCognitoIdentityProviderClient(
                _config.IdentityAccessManagement.AccessKeyId,
                _config.IdentityAccessManagement.SecretAccessKey,
                _config.RegionEndpoint));
        }
    }
}
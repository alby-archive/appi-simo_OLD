namespace AppiSimo.Api.Starting
{
    using Amazon.CognitoIdentityProvider;
    using Areas.Authentication.Abstract;
    using Autofac;
    using Environment;
    using Microsoft.AspNetCore.Hosting;
    using Providers;

    public class HandlerModule : Module
    {
        readonly Cognito _config;
        readonly IHostingEnvironment _env;

        public HandlerModule(Cognito config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => _config);

            builder.Register(b => new AmazonCognitoIdentityProviderClient(
                _config.IdentityAccessManagement.AccessKeyId,
                _config.IdentityAccessManagement.SecretAccessKey,
                _config.RegionEndpoint));

            if (_env.IsDevelopment())
            {
                builder.RegisterType<CognitoUserProviderDisconnected>()
                    .As<IUserProvider>()
                    .SingleInstance();
            }
            else
            {
                builder.RegisterType<CognitoUserProvider>()
                    .As<IUserProvider>()
                    .SingleInstance();
            }
        }
    }
}
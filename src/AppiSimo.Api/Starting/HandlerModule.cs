namespace AppiSimo.Api.Starting
{
    using Amazon.CognitoIdentityProvider;
    using Areas.Authentication.Abstract;
    using Autofac;
    using Providers;
    using Shared.Environment;

    public class HandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CognitoUserProvider>().As<IUserProvider>();

            builder.Register(b =>
            {
                var cognito = b.Resolve<Cognito>();

                return new AmazonCognitoIdentityProviderClient(
                    cognito.IdentityAccessManagement.AccessKeyId,
                    cognito.IdentityAccessManagement.SecretAccessKey,
                    cognito.RegionEndpoint);
            });
        }
    }
}
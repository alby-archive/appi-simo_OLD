namespace AppiSimo.Api.Starting
{
    using Areas.Authentication.Abstract;
    using Areas.Authentication.Providers;
    using Autofac;

    public class RepositoryHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CognitoUserProvider>().As<IUserProvider>();
        }
    }
}
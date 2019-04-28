namespace AppiSimo.Api.Starting
{
    using Autofac;
    using Shared.Environment;

    public class ConfigurationHandlerModule : Module
    {
        readonly Configuration _config;

        public ConfigurationHandlerModule(Configuration config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => _config.Cognito).SingleInstance();
        }
    }
}
namespace AppiSimo.Client.Middelwares
{
    using AppiSimo.Shared.Validators;
    using Microsoft.Extensions.DependencyInjection;

    public static class ValidatorsMiddelWare
    {
        public static void AddValidatorModdleWare(this IServiceCollection services)
        {
            services.AddSingleton(provider => ValidatorProxy.EventsValidator);
        }
    }
}
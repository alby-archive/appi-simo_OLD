namespace AppiSimo.Client.Middleware
{
    using AppiSimo.Shared.Validators;
    using Microsoft.Extensions.DependencyInjection;

    public static class ValidatorsMiddleWare
    {
        public static void AddValidatorMiddleWare(this IServiceCollection services)
        {
            services.AddSingleton(provider => ValidatorProxy.EventsValidator);
        }
    }
}
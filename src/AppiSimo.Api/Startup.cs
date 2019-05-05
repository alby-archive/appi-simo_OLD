namespace AppiSimo.Api
{
    using System;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Shared.Environment;
    using Starting;

    public class Startup
    {
        IConfiguration Configuration { get; }
        ContainerBuilder Builder { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Builder = new ContainerBuilder();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var connection = GetConnectionString();
            var configuration = GetConfiguration();

            services.AddDefaultInjector(configuration.Authority);
            services.AddKingRoger(connection);

            Builder.RegisterModule(new ConfigurationHandlerModule(configuration));
            Builder.RegisterModule(new HandlerModule());
            Builder.Populate(services);

            var container = Builder.Build();

            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperEnvironment(env);
            app.UseAuthentication();
            app.UseRoutesMap();

            app.UseBlazor<Client.Startup>();
        }

        string GetConnectionString() =>
            Heroku.TryParseConnectionString(Environment.GetEnvironmentVariable("DATABASE_URL"))
            ?? Configuration.GetConnectionString("KingRoger_ConnectionString");

        Configuration GetConfiguration()
        {
            var configuration = Configuration.GetSection("Configuration").Get<Configuration>();

            var authority = Heroku.TryParseAuthority(Environment.GetEnvironmentVariable("Authority"));
            var identityAccessManagement = Heroku.TryParseIdentityAccessManagement(Environment.GetEnvironmentVariable("IAM"));

            if (authority != null)
            {
                configuration.Authority = authority;
            }

            if (identityAccessManagement != null)
            {
                configuration.Cognito.IdentityAccessManagement = identityAccessManagement;
            }

            return configuration;
        }
    }
}
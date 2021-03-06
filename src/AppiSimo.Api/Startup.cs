namespace AppiSimo.Api
{
    using System;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Environment;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Starting;

    public class Startup
    {
        IHostingEnvironment Env { get; }
        IConfiguration Configuration { get; }
        ContainerBuilder Builder { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
            Builder = new ContainerBuilder();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var connection = GetConnectionString();
            var configuration = GetConfiguration();

            services.AddKingRoger(connection);

            services.AddDefault(configuration.Authority);
            Builder.RegisterModule(new HandlerModule(configuration.Cognito, Env));
            Builder.Populate(services);

            var container = Builder.Build();

            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperEnvironment(Env);
            app.UseAuthentication();
            app.UseRoutesMap();

            app.UseBlazor<Client.Startup>();
        }

        string GetConnectionString() =>
            Heroku.TryParseConnectionString(System.Environment.GetEnvironmentVariable("DATABASE_URL"))
            ?? Configuration.GetConnectionString("KingRoger_ConnectionString");

        Configuration GetConfiguration()
        {
            var configuration = Configuration.GetSection("Configuration").Get<Configuration>();

            var identityAccessManagement = Heroku.TryParseIdentityAccessManagement(System.Environment.GetEnvironmentVariable("IAM"));

            if (identityAccessManagement != null)
            {
                configuration.Cognito.IdentityAccessManagement = identityAccessManagement;
            }

            return configuration;
        }
    }
}
namespace AppiSimo.Api
{
    using System;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
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
            var connection = Heroku.TryParseConnectionString(System.Environment.GetEnvironmentVariable("DATABASE_URL"))
                             ?? Configuration.GetConnectionString("KingRoger_ConnectionString");

            var awsConfig = Configuration.GetSection("AWS");
            var authConfig = Configuration.GetSection("Authentication");

            services.AddDefaultInjector();
            services.AddConfiguration(awsConfig);
            services.AddKingRoger(connection);
            services.AddAuthentication(authConfig);
            
            Builder.RegisterModule(new RepositoryHandlerModule());
            Builder.Populate(services);

            var container = Builder.Build();

            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperEnvironment(env);
            app.UseRoutesMap();
            app.UseAuthentication();

            app.UseBlazor<Client.Startup>();
        }
    }
}
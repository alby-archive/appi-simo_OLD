namespace AppiSimo.Api
{
    using System;
    using Data;
    using Microsoft.AspNet.OData.Builder;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OData.Edm;
    using Newtonsoft.Json;
    using Shared.Model;

    public class Startup
    {
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<KingRogerContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseNpgsql(GetConnectionString());
            });

            services.AddOData();

            services.AddCors();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(option => option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
                builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc(b =>
            {
                b.Select().Expand().Filter().OrderBy().MaxTop(maxTopValue: 100).Count();
                b.EnableDependencyInjection();
                b.MapODataServiceRoute("odata", "api", GetEdmModel());
            });

            app.Map("/api", api =>
            {
                api.UseMvc(b =>
                {
                    b.MapRoute("default", "{controller}/{action}");
                });
            });

            app.UseBlazor<Client.Startup>();
        }

        
        string GetConnectionString()
            => Heroku.TryParseConnectionString(Environment.GetEnvironmentVariable("DATABASE_URL"))
               ?? Configuration.GetConnectionString("KingRoger_DEV_Database");
        
        static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            builder.EnableLowerCamelCase();

            builder.EntitySet<User>("Users");
            builder.EntitySet<Event>("Events");
            builder.EntitySet<Court>("Courts");
            builder.EntitySet<Light>("Lights");
            builder.EntitySet<Heat>("Heats");
            builder.EntitySet<Rate>("Rates");
            builder.EntitySet<UserEvent>("UserEvent");

            return builder.GetEdmModel();
        }
    }
}
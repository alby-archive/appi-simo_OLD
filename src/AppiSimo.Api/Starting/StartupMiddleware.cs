namespace AppiSimo.Api.Starting
{
    using Microsoft.AspNet.OData.Builder;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.OData.Edm;
    using Shared.Model;

    public static class StartupMiddleware
    {
        public static void UseDeveloperEnvironment(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
        }

        public static void UseRoutesMap(this IApplicationBuilder app)
        {
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            
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
        }
        
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
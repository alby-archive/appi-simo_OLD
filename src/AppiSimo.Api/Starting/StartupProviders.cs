namespace AppiSimo.Api.Starting
{
    using System.IdentityModel.Tokens.Jwt;
    using Data;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Shared.Environment;

    public static class StartupProviders
    {
        public static void AddDefaultInjector(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddOData();
            services.AddCors();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(option => option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        public static void AddKingRoger(this IServiceCollection services, string connection)
        {
            services.AddDbContext<KingRogerContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseNpgsql(connection);
            });
        }

        public static void AddAuthentication(this IServiceCollection services, Authority authority)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(_ => new JwtBearerOptions
                {
                    Audience = authority.Audiences,
                    Authority = authority.EndPoint
                });
        }
    }
}
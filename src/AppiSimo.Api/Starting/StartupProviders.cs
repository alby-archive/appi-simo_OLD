namespace AppiSimo.Api.Starting
{
    using System.IdentityModel.Tokens.Jwt;
    using Data;
    using Environment;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public static class StartupProviders
    {
        public static void AddDefault(this IServiceCollection services, Authority authority)
        {
            services.AddOptions();
            services.AddOData();
            services.AddCors();

            services.AddAuthentication(authority);

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

        static void AddAuthentication(this IServiceCollection services, Authority authority)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = authority.EndPoint;
                    options.Audience = authority.Audience;
                });
        }
    }
}
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApi
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var buider = services.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail = true);

            buider = new IdentityBuilder(buider.UserType, typeof(IdentityRole), services);

            buider.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            var jwtSettings = Configuration.GetSection("Jwt");
            var key = jwtSettings.GetSection("Key").Value;

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.SaveToken = true;
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ValidateAudience = false,
                    };
                });

        }
    }
}

using AspNetCoreRateLimit;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Data;
using MyWebApi.Mail;
using MyWebApi.Models;
using MyWebApi.Services;
using Serilog;
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

        public static void ConfingureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error => {
                error.Run(async context => {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Error($"Error in the {contextFeature.Error}");

                        await context.Response.WriteAsync(new Error
                        {
                            Statuscode = context.Response.StatusCode,
                            Message = "Internal Server Error. Please Try Again Later.",
                        }.ToString());
                    }
                });
            });
        }

        public static void ConfingureHttpCacheHeaders(this IServiceCollection services)
        {
            services.AddResponseCaching();
            services.AddHttpCacheHeaders(
                (expirationOpt) =>
                    {
                        expirationOpt.MaxAge = 180;
                        expirationOpt.CacheLocation = CacheLocation.Private;
                    },
                (validationOpt) =>
                    {
                        validationOpt.MustRevalidate = true;
                    }
                );
        }

        public static void ConfigureServiceHandler(this IServiceCollection services)
        {
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddTransient<ICataLogRespository, CataLogRepository>();
            services.AddTransient<IProductRespository, ProductRespository>();
        }



    }
}

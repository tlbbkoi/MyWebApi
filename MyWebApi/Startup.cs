using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyWebApi.Configurations;
using MyWebApi.Data;
using MyWebApi.IRepository;
using MyWebApi.Repository;
using MyWebApi.Services;
using MyWebApi.Services.MyWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(opitions =>
            {
                opitions.UseSqlServer(Configuration.GetConnectionString("MyApi"));
            });

            services.AddAuthentication();

            services.ConfigureIdentity();

            services.ConfigureJWT(Configuration);

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddCors(o =>
                {
                    o.AddPolicy("AllowAll", builder =>
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                    );
                }
                );

            services.AddAutoMapper(typeof(MapperInitilizer));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthManager, AuthManager>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyWebApi", Version = "v1" });
            });

            services.AddControllers().AddNewtonsoftJson(option => 
                option.SerializerSettings.ReferenceLoopHandling = 
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyWebApi v1"));

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers();
            });
        }
    }
}

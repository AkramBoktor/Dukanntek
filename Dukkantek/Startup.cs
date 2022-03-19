using Dukkantek.Data;
using Dukkantek.Domain.Entities;
using Dukkantek.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dukkantek
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        readonly string CorsPolicy = "CorsPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureIdentity(services);
            ConfigureCors(services);
            ConfigureSwagger(services);

            DependencyResolverUtility.ConfigureServices(services, Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            DatabaseMigratorUtility.MigrateDbContexts(serviceProvider, Configuration);
            AddDefaultMiddlewares(app);
            AddSwaggerConfiguration(app);
            AddEndpoints(app);
        }



        #region Helper
        // ConfigureServices
        private void ConfigureIdentity(IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

           
        }
        private void ConfigureCors(IServiceCollection services)
        {
            var list = new List<string>();
            Configuration.GetSection("AllowedHosts").Bind(list);
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy, builder =>
                {
                    builder.WithOrigins(list.ToArray()).SetIsOriginAllowed(x => _ = true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });

            });
        }
        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "System Swagger",
                    Description = "Api documentation",
                    Version = "1"
                });

      

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        // Configure
      
        private void AddLoggerConfiguration(ILoggerFactory loggerFactory)
        {
            int retainedLogFileCountLimit = Configuration.GetValue<int>("RetainedLogFileCountLimit");
            if (retainedLogFileCountLimit > 0)
                loggerFactory.AddFile("Logs/Api-Logging-{Date}.txt", retainedFileCountLimit: retainedLogFileCountLimit);
            else
                loggerFactory.AddFile("Logs/Api-Logging-{Date}.txt");


        }
        private void AddDefaultMiddlewares(IApplicationBuilder app)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-5.0
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(CorsPolicy);
            app.UseAuthentication();
            app.UseAuthorization();
        }
        private void AddSwaggerConfiguration(IApplicationBuilder app)
        {
            app.UseSwagger(options => options.RouteTemplate = "docs/{documentName}/docs.json");
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/docs/v1/docs.json", "Swagger Document");
                options.RoutePrefix = "docs";
            });
        }
        private void AddEndpoints(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
        #endregion
    }
}


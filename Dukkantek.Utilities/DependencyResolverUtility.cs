using System;
using System.IO;
using AutoMapper;
using Dukkantek.Domain.Helper;
using Dukkantek.Domain.Interfaces.Mapper;
using Dukkantek.Domain.Repositories;
using Dukkantek.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using Dukkantek.Infrastructure.Repositories;
using Dukkantek.Data;
using Microsoft.AspNetCore.Http.Features;
using Gezira.Infrastructure.Repositories;
using Dukkantek.Service.Services;
using Dukkantek.service.Helper;
using Gezira.Service.Mapper;
using Microsoft.AspNetCore.Http;
using Dukkantek.Service.Mapper;

namespace Dukkantek.Utilities
{
    public class DependencyResolverUtility
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            Setup(services, configuration);
            
            RegisterCache(services, configuration);
            RegisterDbContexts(services, configuration);
            RegisterRepositories(services, configuration);
            RegisterServices(services, configuration);
            RegisterCoreServices(services, configuration);
            RegisterHelpers(services, configuration);
            RegisterMapper(services, configuration);
        }
        private static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            //.. Setup ..//
            var maxRequestSize = Convert.ToInt32(configuration["MaxRequestSize"]);
            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.MaxRequestBodySize = maxRequestSize;
            //    options.AllowSynchronousIO = true;
            //});

            services.Configure<FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = maxRequestSize;
            });

            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        }
        private static void RegisterCache(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton(typeof(ICacheManagerConfiguration),
            //    new CacheManager.Core.ConfigurationBuilder()
            //        .WithJsonSerializer()
            //        .WithMicrosoftMemoryCacheHandle()
            //        .Build());
        }
        private static void RegisterDbContexts(IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<DukkantekContext>();
            services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);
        }
        private static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserManagerRepository, IdentityManagerRepository>();
            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
      
        }
        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<Domain.Interfaces.Services.ITokenService, TokenService>();
            services.AddScoped<IProductService, ProductService>();
        
        }
        private static void RegisterHelpers(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IChecker, Checker>();
        }
        private static void RegisterMapper(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMapperStore, MapperStore>();
            services.AddScoped<IUserMapper, UserMapper>();
            services.AddScoped< IProductMapper,ProductMapper >();
            


            var configurationMappings = MapperConfigurator.ConfigureMappings(configuration);
            services.AddSingleton<IMapper>(new Mapper(configurationMappings));
        }
        private static void RegisterCoreServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<INetworkCommunicator, NetworkCommunicator>();
            //services.AddScoped<IWebRequestUtility, WebRequestUtility>();
            //services.AddScoped<IConfigurationManager<>, ConfigurationManager>();
            //services.AddScoped<IExportExcelManager, ExportExcelManager>();
        }
    }
}

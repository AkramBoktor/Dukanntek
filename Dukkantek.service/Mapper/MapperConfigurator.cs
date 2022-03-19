using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;


namespace Gezira.Service.Mapper
{
    public class MapperConfigurator : Profile
    {
        public static MapperConfiguration ConfigureMappings(IConfiguration configuration)
        {
            var mapperConfiguration = new MapperConfiguration(mapperConfigs =>
            {

            });

            return mapperConfiguration;
        }


    }
}
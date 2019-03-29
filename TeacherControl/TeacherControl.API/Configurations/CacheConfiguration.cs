using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherControl.API.Configurations
{
    public static class CacheConfiguration
    {
        public static IServiceCollection ConfigureCaching(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services
                .AddDistributedRedisCache(config => {
                    config.Configuration = configuration.GetSection("ConnectionStrings:TeacherControlCache").Value;
                    config.InstanceName = "TC_Redis_Cache";
                })
                .ConfigureResponseCaching();
            return services;
        }

        private static IServiceCollection ConfigureResponseCaching(this IServiceCollection services)
        {

            return services;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using TeacherControl.DataEFCore;
using TeacherControl.Domain.Services;

namespace TeacherControl.API.Configurations
{
    public static class ConnectionConfiguration
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = string.Empty;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connection = configuration.GetConnectionString("TeacherControlDbWindows") ??
                                 "<FALLBACK_SQL_SERVER_CONNECTION_STRING_HERE>";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connection = configuration.GetConnectionString("TeacherControlDbDocker") ??
                                 "<FALLBACK_DOCKER_SQL_SERVER_CONNECTION_STRING_HERE>";
            }

            services
                .AddTransient<IUserService, DummyUserService>() //TODO: add a service that can retieve the user's info 
                .AddDbContext<TCContext>(options => options.UseSqlServer(connection).UseLazyLoadingProxies());

            return services;
        }

        class DummyUserService : IUserService
        {
            public string GetUsername() => "TEST_DUMMY_USER";
        }

    }
}

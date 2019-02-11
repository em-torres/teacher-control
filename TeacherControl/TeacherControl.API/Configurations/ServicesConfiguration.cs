using AutoMapper;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Reflection;
using TeacherControl.DataEFCore;
using TeacherControl.DataEFCore.Repositories;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Configurations
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IAssignmentRepository, AssignmentRepository>()
                .AddScoped<ICourseRepository, CourseRepository>()
                .AddScoped<IGroupRepository, GroupRepository>()
                .AddScoped<IStatusRepository, StatusRepository>()
                .AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .Build());
            });

            return services;
        }

        public static IServiceCollection AddPluralizationService(this IServiceCollection services)
        {
            services.AddSingleton<IPluralizer, DbTablePluralizer>();

            return services;
        }

        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfiles("TeacherControl.Domain"));

            return services;
        }


    }
}

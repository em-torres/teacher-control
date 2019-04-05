using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Queries;
using TeacherControl.Domain.DTOsValidations;

namespace TeacherControl.API.Configurations
{
    public static class FluentValidationConfiguration
    {

        public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services
                .AddFluentDTOsValidationRules();

            return services;
        }

        private static IServiceCollection AddFluentDTOsValidationRules(this IServiceCollection services)
        {
            services
                .AddTransient<IValidator<AssignmentDTO>, AssingmentDTOValidation>()
                .AddTransient<IValidator<CourseDTO>, CourseDTOValidation>();

            return services;
        }

    }
}

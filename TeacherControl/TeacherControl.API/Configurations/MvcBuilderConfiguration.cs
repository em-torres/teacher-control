using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.DTOsValidations;

namespace TeacherControl.API.Configurations
{
    public static class MvcBuilderConfiguration
    {
        public static IMvcBuilder AddFluentValidationConfiguration(this IMvcBuilder builder)
        {
            builder
                .AddFluentValidation(config =>
                {
                    config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    //config.ImplicitlyValidateChildProperties = true; //TODO: TBD
                });

            return builder;
        }

        public static IServiceCollection AddFluentDTOsValidationRules(this IServiceCollection services)
        {
            services.AddTransient<IValidator<AssignmentDTO>, AssingmentDTOValidation>();

            return services;
        }

        public static IServiceCollection AddFluentQueryFiltersValidationRules(this IServiceCollection services)
        {
            //services.AddTransient<IValidator<AssignmentDTO>, AssingmentDTOValidation>();

            return services;
        }
        
    }
}

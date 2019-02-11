using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace TeacherControl.API.Configurations
{
    public static class MvcBuilderConfiguration
    {
        public static IMvcBuilder AddFluentValidationConfiguration(this IMvcBuilder builder)
        {
            builder
                .AddFluentValidation(config => {
                    config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    //config.RegisterValidatorsFromAssemblyContaining<AbstractValidator>();
                });

            return builder;
        }
    }
}

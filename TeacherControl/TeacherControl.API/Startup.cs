using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TeacherControl.API.Configurations;

namespace TeacherControl.API
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                   .SetBasePath(env.ContentRootPath)
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                   .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO: loging svc log4netor with the MS ILogger
            services
                .AddAutoMapperConfiguration()
                .AddConnectionProvider(Configuration)
                .ConfigureRepositories()
                .AddPluralizationService()
                .AddMiddlewares()
                .AddCorsConfiguration()
                .AddFluentDTOsValidationRules()
                .AddFluentQueryFiltersValidationRules()
                .ConfigureBearerAuthentication(Configuration)
                .ConfigureCaching(Configuration)
                .Configure<AppSettings>(Configuration.GetSection("AppSettings"))
                .AddOptions<AppSettings>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.ConfigureSecurePolicies()
                .UseAuthentication()
                .UseMvcWithDefaultRoute(); 
            //TODO: redirect to the default route (HomeCOntroller) if we got a 404 route
            //and also on the start up redirect to the HomeController
        }


    }
}

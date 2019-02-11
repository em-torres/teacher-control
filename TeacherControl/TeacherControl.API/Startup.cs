using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeacherControl.API.Configurations;

namespace TeacherControl.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMemoryCache();

            //services.AddResponseCaching();

            //TODO: loging svc log4net
            services
                .AddAutoMapperConfiguration()
                .AddConnectionProvider(Configuration)
                .ConfigureRepositories()
                .AddPluralizationService()
                .AddMiddlewares()
                .AddCorsConfiguration();

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidationConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes => routes.MapRoute("api", "api/{controller}/{id:int:min(1)?}"));//TODO: check if this is right
        }

    }
}

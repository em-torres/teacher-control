using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using TeacherControl.Domain.Services;
using TeacherControl.Infraestructure.Services;

namespace TeacherControl.API.Configurations
{
    public static class AuthConfiguration
    {
        public static IServiceCollection ConfigureBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            byte[] SECRET_KEY = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value);
            string AUDIENCE = configuration.GetSection("AppSettings:Audience").Value;
            string ISSUER = configuration.GetSection("AppSettings:Issuer").Value;

            services
                .AddScoped<IAuthUserService, AuthUserService>()
                .AddAuthentication(config =>
                {
                    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config =>
                {
                    //config.Audience = AUDIENCE;
                    //config.Authority = ISSUER;
                    config.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = (contex) =>
                        {
                            //TODO logger here

                            var userService = contex.HttpContext.RequestServices.GetRequiredService<IAuthUserService>();
                            userService.Username = contex.Principal.Identity.Name;
                            //add claims here for the user here

                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = (context) =>
                        {
                            //TODO logger here to log 
                            //context.Exception.Message

                            context.Fail(new Exception("On validated bearer token error"));
                            return Task.CompletedTask;
                        }
                    };

                    config.SaveToken = true;
                    config.RequireHttpsMetadata = false; //TODO set this flag to TRUE on prod
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        //TBD, should i pass the issuer and the audience?
                        
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(SECRET_KEY),
                        //ValidateIssuer = true,
                        //ValidateAudience = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.FromMinutes(1),
                    };

                });

            return services;
        }
    }
}

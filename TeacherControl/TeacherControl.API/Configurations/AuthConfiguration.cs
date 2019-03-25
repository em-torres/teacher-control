using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherControl.Domain.Services;
using TeacherControl.Infraestructure.Services;

namespace TeacherControl.API.Configurations
{
    public static class AuthConfiguration
    {
        public static IServiceCollection ConfigureBearerAuthentication(this IServiceCollection services)
        {
            byte[] SECRET_KEY = Encoding.ASCII.GetBytes("PUT_SECRET_KEY_HERE_FROM_THE_CONFIG");

            services
                //.AddScoped<IAuthUserService, AuthUserService>()
                .AddScoped<IAuthUserService, DummyAuthUserService>()
                .AddAuthentication(config =>
                {
                    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config => 
                {
                    //config.Audience = "get from the config";
                    //config.Authority = "get from the config";
                    config.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = (contex) =>
                        {
                            //TODO logger here

                            //IUserService userService = contex.HttpContext.RequestServices.GetRequiredService<IUserService>();
                            //contex.Principal.Identity.Name userId = userService.GetUsername();

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
                        //TBD, shouldi pass the issuer and the audience?
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(SECRET_KEY),
                        //ValidateIssuer = true,
                        //ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ClockSkew = TimeSpan.FromMinutes(5),
                    };

                });

            return services;
        }
    }

    //TODO: delete this
    class DummyAuthUserService : IAuthUserService
    {
        public string Username { get => "TEST_DUMMY_USERNAME"; set => throw new NotImplementedException(); }
    }

}

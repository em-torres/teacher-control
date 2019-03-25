using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherControl.API.Configurations
{
    public static class SecurityConfiguration
    {
        public static IApplicationBuilder ConfigureSecurePolicies(this IApplicationBuilder builder)
        {

            return builder;
        }
    }
}

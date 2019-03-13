using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Common.Helpers
{
    public static class EnvironmentHelper
    {
        public static bool IsProduction() => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower().Equals("production");
        public static bool IsDevelopment() => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower().Equals("development");
    }
}

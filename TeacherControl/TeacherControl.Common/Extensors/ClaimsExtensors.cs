using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace TeacherControl.Common.Extensors
{
    public static class ClaimsExtensors
    {
        public static string GetUsername(this IEnumerable<Claim> claims) => claims is null
            ? null
            : claims.Where(i => i.Type.ToLower().Equals("username")).First().Value;
    }
}

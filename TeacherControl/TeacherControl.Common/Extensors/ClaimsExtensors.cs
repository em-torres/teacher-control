using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace TeacherControl.Common.Extensors
{
    public static class ClaimsExtensors
    {
        public static string GetByName(this IEnumerable<Claim> Claims, string Name)
            => Claims.Where(i => i.Type.ToLower().Equals(Name.ToLower())).First().Value;
    }
}

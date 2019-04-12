using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.Queries
{
    public class UserQuery : BaseQuery
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string CodeId { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string ByRoleName { get; set; }
        public DateTime FromBirthday { get; set; }
        public DateTime EndBirthday { get; set; }
    }
}

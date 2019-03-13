using System;
using System.Collections.Generic;

namespace TeacherControl.Domain.Models
{
    public class User : BaseModel
    {
        public string AuthToken { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<UserGroup> Groups { get; set; }
        public virtual ICollection<UserCourse> Courses{ get; set; }
    }
}

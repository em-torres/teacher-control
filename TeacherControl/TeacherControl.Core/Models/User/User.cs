using System;
using System.Collections.Generic;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class User : IModificationAudit
    {
        public int Id { get; set; }
        public string AuthToken { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<UserGroup> Groups { get; set; }
        public virtual ICollection<UserCourse> Courses{ get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

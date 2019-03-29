using System;
using System.Collections.Generic;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class User : IModificationAudit, IStatusAudit
    {
        public int Id { get; set; }
        public string SaltToken { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public virtual UserInfo UserInfo { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<CourseUserCredit> Credits { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public string Status { get; set; }
    }
}

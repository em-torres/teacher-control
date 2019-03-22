using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class UserCourse : IModificationAudit
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

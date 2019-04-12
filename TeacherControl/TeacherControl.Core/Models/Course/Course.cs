using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class Course : IModificationAudit, IStatusAudit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CodeId { get; set; }
        public string HashIndex { get; set; }
        public string Description { get; set; }
        public double Credits { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        public int ProfessorId { get; set; }
        public virtual User Professor { get; set; }

        public virtual ICollection<CourseUserCredit> StudentCredits { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<CourseTag> Tags { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

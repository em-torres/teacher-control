using System;
using System.ComponentModel.DataAnnotations;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class AssignmentStudentPoint : IModificationAudit
    {
        public int Id { get; set; }
        public float Points { get; set; }
        public virtual Assignment Assignment { get; set; }
        public virtual User Student { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

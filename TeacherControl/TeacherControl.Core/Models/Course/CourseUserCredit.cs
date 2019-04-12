using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class CourseUserCredit : IModificationAudit
    {
        //TBD: Add a status here for the user credits?
        public int Id { get; set; }
        public double Credits { get; set; }
        public int StudentId { get; set; }
        public virtual User Student { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

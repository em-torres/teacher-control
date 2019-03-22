using System;
using System.Collections.Generic;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class Assignment : IModificationAudit, IStatusAudit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HashIndex { get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }
        public string Body { get; set; }
        public double Points { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public virtual AssignmentCounts Counts{ get; set; }
        public virtual ICollection<AssignmentTag> Tags { get; set; }
        public virtual ICollection<AssignmentComment> Comments { get; set; }
        public virtual ICollection<Questionnaire> Questionnaires { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class Questionnaire : IModificationAudit, IStatusAudit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public float Points { get; set; }
        public string Status { get; set; }

        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }
        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<QuestionnaireCommitment> QuestionnaireCommitments { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

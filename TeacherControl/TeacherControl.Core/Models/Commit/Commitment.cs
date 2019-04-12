using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class Commitment : IModificationAudit, IStatusAudit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<UserAnswer> Answers { get; set; }
        public virtual ICollection<UserAnswerMatch> Matches { get; set; }
        public virtual ICollection<UserOpenResponseAnswer> OpenResponses { get; set; }

        public virtual ICollection<QuestionnaireCommitment> AssignmentCommitments { get; set; }

        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

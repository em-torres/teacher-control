using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class Commitment : BaseModel
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<UserAnswer> Answers { get; set; }
        public virtual ICollection<UserAnswerMatch> Matches { get; set; }
        public virtual ICollection<UserOpenResponseAnswer> OpenResponses { get; set; }

        public virtual ICollection<QuestionnaireCommitment> AssignmentCommitments { get; set; }
    }
}

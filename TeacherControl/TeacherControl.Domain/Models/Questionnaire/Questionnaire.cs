using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class Questionnaire : BaseModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public float Points { get; set; }
        public virtual Status Status { get; set; }

        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }
        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<QuestionnaireCommitment> QuestionnaireCommitments { get; set; }
    }
}

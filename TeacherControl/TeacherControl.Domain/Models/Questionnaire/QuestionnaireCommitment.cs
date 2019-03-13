using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class QuestionnaireCommitment
    {
        public int QuestionnaireId { get; set; }
        public Questionnaire Questionnaire{ get; set; }

        public int CommitmentId { get; set; }
        public Commitment Commitment { get; set; }
    }
}

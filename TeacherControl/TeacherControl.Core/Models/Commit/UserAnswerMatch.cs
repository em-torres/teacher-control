using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.Models
{
    public class UserAnswerMatch
    {
        public int Id { get; set; }
        public int LeftQuestionAnswerId { get; set; }
        public int RightQuestionAnswerId { get; set; }

        public int QuestionAnswerId { get; set; }
        public virtual QuestionAnswer QuestionAnswer { get; set; }

        public int CommitmentId { get; set; }
        public virtual Commitment Commitment { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class QuestionAnswerMatch
    {
        public int Id { get; set; }
        public int LeftQuestionAnswerId { get; set; }
        public int RightQuestionAnswerId { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}

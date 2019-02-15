using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class QuestionAnswerUserMatch
    {
        public int Id { get; set; }
        public QuestionAnswer LeftQuestionAnswer { get; set; }
        public QuestionAnswer RightQuestionAnswer { get; set; }
        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class QuestionAnswerMatch
    {
        public int Id { get; set; }
        public QuestionAnswer LeftQuestionAnswer { get; set; }
        public QuestionAnswer RightQuestionAnswer { get; set; }
        
    }
}

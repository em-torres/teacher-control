using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.DTOs
{
    public class QuestionAnswerMatchDTO
    {
        public QuestionAnswerDTO LeftQuestionAnswer { get; set; }
        public QuestionAnswerDTO RightQuestionAnswer { get; set; }
    }
}

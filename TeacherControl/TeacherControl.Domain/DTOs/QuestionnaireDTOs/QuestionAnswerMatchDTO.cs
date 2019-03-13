using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.DTOs
{
    public class QuestionAnswerMatchDTO
    {
        public QuestionAnswerDTO LeftQuestionAnswer { get; set; }
        public QuestionAnswerDTO RightQuestionAnswer { get; set; }
    }
}

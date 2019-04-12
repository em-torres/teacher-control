using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.DTOs
{
    public class UserAnswerMatchDTO
    {
        public int LeftQuestionAnswer { get; set; }
        public int RightQuestionAnswer { get; set; }
        public int QuestionAnswer { get; set; }
        public int Commitment { get; set; }
    }
}

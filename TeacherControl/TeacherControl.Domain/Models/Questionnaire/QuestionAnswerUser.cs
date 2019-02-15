using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class QuestionAnswerUser
    {
        public int Id { get; set; }
        public QuestionAnswer QuestionAnswer { get; set; }
        public User User { get; set; }
    }
}

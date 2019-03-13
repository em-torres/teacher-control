using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }

        public int QuestionAnswerId { get; set; }
        public QuestionAnswer QuestionAnswer { get; set; }
        
        public int CommitmentId { get; set; }
        public Commitment Commitment { get; set; }
    }
}

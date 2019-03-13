using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }

        public int QuestionAnswerId { get; set; }
        public virtual QuestionAnswer QuestionAnswer { get; set; }
        
        public int CommitmentId { get; set; }
        public virtual Commitment Commitment { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.Models
{
    public class UserOpenResponseAnswer
    {
        public int Id { get; set; }
        public string UserResponse { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public int CommitmentId { get; set; }
        public virtual Commitment Commitment { get; set; }
    }
}

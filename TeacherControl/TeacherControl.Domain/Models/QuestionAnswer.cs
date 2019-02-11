using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MaxLength { get; set; }
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}

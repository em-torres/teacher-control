using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherControl.Domain.DTOs
{
    public class QuestionAnswerDTO
    {
        public string Title { get; set; }
        public bool IsCorrect { get; set; }
        public int MaxLength { get; set; }
    }
}

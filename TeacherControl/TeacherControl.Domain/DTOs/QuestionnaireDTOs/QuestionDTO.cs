using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.DTOs
{
    public class QuestionDTO
    {
        public string HeadLine { get; set; }
        public double Points { get; set; }
        public bool IsRequired { get; set; }

        public IEnumerable<QuestionAnswerDTO> Answers { get; set; }
    }
}

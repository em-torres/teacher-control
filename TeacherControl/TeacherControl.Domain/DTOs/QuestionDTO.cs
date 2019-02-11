using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherControl.Domain.DTOs
{
    public class QuestionDTO
    {
        public string Title { get; set; }
        public double Points { get; set; }
        public bool IsRequired { get; set; }

        public IEnumerable<QuestionAnswerDTO> Answers { get; set; }
    }
}

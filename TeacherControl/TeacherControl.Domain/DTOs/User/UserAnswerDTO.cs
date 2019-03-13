using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.DTOs
{
    public class UserAnswerDTO
    {
        public IEnumerable<QuestionAnswerDTO> Answers { get; set; }
        public IEnumerable<QuestionAnswerMatchDTO> Matches { get; set; }
        public IEnumerable<QuestionOpenResponseDTO> OpenResponses { get; set; }

    }
}

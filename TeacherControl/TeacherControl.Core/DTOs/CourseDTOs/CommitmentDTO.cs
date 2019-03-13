using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.DTOs
{
    public class CommitmentDTO
    {
        public QuestionnaireDTO Questionnaire { get; set; }
        public IEnumerable<UserAnswerDTO> Answers { get; set; }
        public IEnumerable<UserAnswerDTO> Matches { get; set; }
        public IEnumerable<UserAnswerDTO> OpenResponse { get; set; }
    }
}

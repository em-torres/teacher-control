using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.DTOs.CommitDTOs
{
    public class CommitmentDTO
    {
        public int User { get; set; }
        public virtual IEnumerable<UserAnswerDTO> Answers { get; set; }
        public virtual IEnumerable<UserAnswerMatchDTO> Matches { get; set; }
        public virtual IEnumerable<UserOpenResponseDTO> OpenResponses { get; set; }
    }
}


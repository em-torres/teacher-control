using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherControl.Domain.DTOs
{
    public class QuestionnaireDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int Status { get; set; }

        public IEnumerable<QuestionnaireDTO> Sections { get; set; }
    }
}

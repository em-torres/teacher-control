using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherControl.Domain.Enums;

namespace TeacherControl.Domain.DTOs
{
    public class QuestionnaireDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public float PointsToPass { get; set; }
        public DateTime StartDate { get; set; }
        public Status Status { get; set; }

        public IEnumerable<QuestionDTO> Questions { get; set; }

    }
}

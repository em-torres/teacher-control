using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.DTOs
{
    public class AssignmentResultDTO
    {
        public string Title { get; set; }
        public double PointsToPass { get; set; }
        public double TotalUserPoints { get; set; }
        public bool IsPassed { get; set; }
        public IEnumerable<QuestionnaireResultDTO> Results { get; set; }

    }
}

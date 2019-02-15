using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Queries
{
    public class QuestionnaireQuery
    {
        public int Title { get; set; }
        public int Points { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public float StartPointsToPass { get; set; }
        public float EndPointsToPass { get; set; }
    }
}

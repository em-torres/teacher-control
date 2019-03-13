using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.DTOs
{
    public class QuestionnaireResultDTO
    {
        public string Title { get; set; }
        public double QuestionnairePoints { get; set; }
        public double UserPoints { get; set; }
        public bool IsPassed { get; set; }
    }
}

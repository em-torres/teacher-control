using System;
using System.Collections.Generic;
using TeacherControl.Core.Enums;

namespace TeacherControl.Core.DTOs
{
    public class AssignmentDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public double Points { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}

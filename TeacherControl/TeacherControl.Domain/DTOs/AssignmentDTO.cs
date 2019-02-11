using System;
using System.Collections.Generic;
using TeacherControl.Domain.DTOs;

namespace TeacherControl.Domain.DTOs
{
    public class AssignmentDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public double Points { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }

        // relationships
        public ICollection<string> Tags { get; set; }
        public ICollection<string> Types { get; set; }
        public ICollection<string> Groups { get; set; }
    }
}

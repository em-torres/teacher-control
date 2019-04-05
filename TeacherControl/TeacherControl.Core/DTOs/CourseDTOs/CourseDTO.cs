using System;
using System.Collections.Generic;

namespace TeacherControl.Core.DTOs
{
    public class CourseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Credits { get; set; }
        public string CodeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Professor { get; set; }
        public ICollection<string> Tags { get; set; }
        //public ICollection<CourseStudentDTO> Students { get; set; }
    }
}

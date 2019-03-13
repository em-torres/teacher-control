using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherControl.Domain.DTOs
{
    public class CourseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Credits { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Professor { get; set; }
        public string Tags { get; set; }
        public int Status { get; set; }
    }
}

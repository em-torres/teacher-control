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
        public float Credits { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual int Professor { get; set; }
        public virtual ICollection<string> Tags { get; set; }
        public virtual ICollection<string> Types { get; set; }
        public virtual int Status { get; set; }
    }
}

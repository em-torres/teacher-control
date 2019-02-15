using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class Course : BaseModel
    {
        public string Name { get; set; }
        public string HashIndex { get; set; }
        public string Description { get; set; }
        public float Credits { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual User Professor { get; set; }
        public virtual ICollection<CourseTag> Tags { get; set; }
        public virtual Status Status { get; set; }

    }
}

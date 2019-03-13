using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class Course : BaseModel
    {
        public string Name { get; set; }
        public string CodeIdentifier { get; set; }
        public string HashIndex { get; set; }
        public string Description { get; set; }
        public double Credits { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<UserCourse> Professors { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<CourseComment> Comments { get; set; }
        public virtual ICollection<CourseTag> Tags { get; set; }


    }
}

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
        public virtual ICollection<CourseType> Types { get; set; }
        public virtual Status Status { get; set; }

        public Course(string name, string description, float credits, DateTime startDate, DateTime endDate, User professor, Status Status)
        {
            Name = name;
            Description = description;
            Credits = credits;
            StartDate = startDate;
            EndDate = endDate;
            Professor = professor;
            Tags = new HashSet<CourseTag>();
            Types = new HashSet<CourseType>();
            this.Status = Status;
        }

        public Course()
        {
            Name = string.Empty;
            Description = string.Empty;
            Credits = 0f;
            StartDate = DateTime.UtcNow;
            EndDate = DateTime.UtcNow;
            Professor = new User();
            Tags = new HashSet<CourseTag>();
            Types = new HashSet<CourseType>(); ;
        }
    }
}

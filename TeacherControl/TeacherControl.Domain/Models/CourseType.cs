using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class CourseType : BaseModel
    {
        public string Name { get; set; }
        public string Description{ get; set; }
        public virtual Course Course { get; set; }

        public CourseType(string name, string description, Course course)
        {
            Name = name;
            Description = description;
            Course = course;
        }

        public CourseType()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

    }
}

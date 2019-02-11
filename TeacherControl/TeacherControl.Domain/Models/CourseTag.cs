using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class CourseTag : BaseModel
    {
        public string Name { get; set; }
        public virtual Course Course { get; set; }

        public CourseTag(string name, Course course)
        {
            Name = name;
            Course = course;
        }

        public CourseTag()
        {
            Name = string.Empty;
            Course = new Course();
        }
    }
}

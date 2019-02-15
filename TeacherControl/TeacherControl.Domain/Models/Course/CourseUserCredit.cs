using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class CourseUserCredit : BaseModel
    {
        public float Credits { get; set; }
        public virtual User Student { get; set; }
        public virtual Course Course { get; set; }

        public CourseUserCredit(User student, Course course, float credits)
        {
            Student = student;
            Course = course;
            Credits = credits;
        }

        public CourseUserCredit()
        {
            Student = new User();
            Course = new Course();
            Credits = 0f;
        }
    }
}

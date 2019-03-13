using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class UserCourse
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}

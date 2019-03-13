using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class CourseUserCredit : BaseModel
    {
        public float Credits { get; set; }
        public int StudentId { get; set; }
        public virtual User Student { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class CourseTag : BaseModel
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

    }
}

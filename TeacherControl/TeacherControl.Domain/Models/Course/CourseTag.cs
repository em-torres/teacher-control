using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class CourseTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Queries
{
    public class CourseQuery : BaseQuery
    {
        public string Name { get; set; }
        public float ExactCredits { get; set;  }
        public float StartFrom { get; set; }
        public float EndFrom { get; set; }
    }
}

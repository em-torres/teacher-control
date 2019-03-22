using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.Enums;

namespace TeacherControl.Core.Queries
{
    public class AssignmentQuery : BaseQuery
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float StartPoints { get; set; }
        public float EndPoints { get; set; }
        public string Types { get; set; }
        public string Tags { get; set; }
        public Status Status { get; set; }
    }
}

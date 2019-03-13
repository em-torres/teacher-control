using System;
using System.Collections.Generic;
using TeacherControl.Core.Enums;

namespace TeacherControl.Core.Queries
{
    public class CourseQuery : BaseQuery
    {
        public string Name { get; set; }
        public double CreditsFrom { get; set; }
        public double CreditsEnd { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Professor { get; set; }
        public Status Status { get; set; }
        public string Tags { get; set; }
    }
}

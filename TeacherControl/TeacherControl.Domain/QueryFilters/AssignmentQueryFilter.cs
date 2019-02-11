using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Common.Enums;

namespace TeacherControl.Domain.QueryFilters
{
    public class AssignmentQueryFilter
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float StartPoints { get; set; }
        public float EndPoints { get; set; }
        public string Groups { get; set; }
        public string Types { get; set; }
        public string Tags { get; set; }
        public float Points { get; set; }
        public Status Status { get; set; }
    }
}

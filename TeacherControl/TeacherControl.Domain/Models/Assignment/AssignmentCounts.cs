using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class AssignmentCounts
    {
        public int Id { get; set; }
        public int UpvotesCount { get; set; }
        public int ViewsCount { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.Models
{
    public class AssignmentCounts
    {//TODO: create another model to check who made the up/down vote...
        public int Id { get; set; }
        public int UpvotesCount { get; set; }
        public int ViewsCount { get; set; }

        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}

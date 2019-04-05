using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.Queries
{
    public class AssignmentCommentQuery : BaseQuery
    {
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

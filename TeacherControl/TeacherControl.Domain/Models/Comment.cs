using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class Comment : BaseModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Upvote { get; set; }

        public virtual User Author { get; set; }

        public virtual IEnumerable<AssignmentComment> Assignments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class CourseComment : BaseModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int Upvote { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}

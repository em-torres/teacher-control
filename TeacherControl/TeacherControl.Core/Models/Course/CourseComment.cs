using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class CourseComment : IModificationAudit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Upvote { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

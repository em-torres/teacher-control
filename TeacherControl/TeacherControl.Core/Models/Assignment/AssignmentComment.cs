using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class AssignmentComment : IModificationAudit, IStatusAudit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public virtual ICollection<AssignmentCommentUpvote> Upvotes { get; set; }
        public virtual ICollection<AssignmentCommentDownvote> Downvotes { get; set; }

        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class AssignmentComment : BaseModel
    {
        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
    }
}

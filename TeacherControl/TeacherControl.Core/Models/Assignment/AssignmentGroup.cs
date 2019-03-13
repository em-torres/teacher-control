using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.Models
{
    public class AssignmentGroup
    {
        public virtual Assignment Assignment { get; set; }
        public virtual Group Group { get; set; }

        public int AssignmentId { get; set; }
        public int GroupId { get; set; }
    }
}

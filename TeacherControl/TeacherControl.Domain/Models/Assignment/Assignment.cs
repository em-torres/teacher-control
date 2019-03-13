using System;
using System.Collections.Generic;

namespace TeacherControl.Domain.Models
{
    public class Assignment : BaseModel
    {
        public string Title { get; set; }
        public string HashIndex { get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }
        public string Body { get; set; }
        public double Points { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public virtual AssignmentCounts Counts{ get; set; }
        public virtual ICollection<AssignmentTag> Tags { get; set; }
        public virtual ICollection<AssignmentGroup> Groups { get; set; }
        public virtual ICollection<Questionnaire> Questionnaires { get; set; }
    }
}
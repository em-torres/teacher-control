using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        // relationships
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public AssignmentCounts Counts{ get; set; }
        public virtual ICollection<AssignmentTag> Tags { get; set; }
        public virtual ICollection<AssignmentGroup> Groups { get; set; }
        public virtual ICollection<AssignmentComment> Comments { get; set; }
        public virtual ICollection<AssignmentCarousel> Carousels { get; set; }
        public virtual ICollection<Questionnaire> Questionnaires { get; set; }

        public Assignment()
        {
            Tags = new HashSet<AssignmentTag>();
            Groups = new HashSet<AssignmentGroup>();
            Questionnaires = new HashSet<Questionnaire>();
            Carousels = new HashSet<AssignmentCarousel>();
            Comments = new HashSet<AssignmentComment>();
            Status = new Status();
        }
    }
}
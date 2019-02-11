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
        public int UpvotesCount { get; set; }
        public int ViewsCount { get; set; }

        // relationships
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public virtual IEnumerable<AssignmentTag> Tags { get; set; }
        public virtual IEnumerable<AssignmentType> Types { get; set; }
        public virtual IEnumerable<AssignmentGroup> Groups { get; set; }
        public virtual IEnumerable<AssignmentComment> Comments { get; set; }
        public virtual IEnumerable<AssignmentCarousel> Carousels { get; set; }
        public virtual IEnumerable<Questionnaire> Questionnaires { get; set; }

        public Assignment()
        {
            Tags = new HashSet<AssignmentTag>();
            Types = new HashSet<AssignmentType>();
            Groups = new HashSet<AssignmentGroup>();
            Questionnaires = new HashSet<Questionnaire>();
            Carousels = new HashSet<AssignmentCarousel>();
            Comments = new HashSet<AssignmentComment>();
            Status = new Status();
        }
    }
}
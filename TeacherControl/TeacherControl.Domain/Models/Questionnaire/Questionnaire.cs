using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    /*
     * relationship starts here
     * [n-m]assignment(parent)
     * questionnaire
     * |[n-1]--|--questions
     * |[n-1]-----|--answers
    */
    public class Questionnaire : BaseModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public float PointsToPass { get; set; }
        public virtual Status Status { get; set; }

        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; }
    }
}

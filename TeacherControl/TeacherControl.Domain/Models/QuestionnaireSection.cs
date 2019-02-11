using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class QuestionnaireSection
    {
        public int Id { get; set; }
        public int Page { get; set; }

        public int QuestionnaireId { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; }
        public virtual Questionnaire Questionnaire { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string HeadLine { get; set; }
        public double Points { get; set; }
        public bool IsRequired { get; set; }
        public int Order { get; set; }

        public int QuestionnaireId { get; set; }
        public virtual Questionnaire Questionnaire { get; set; }

        public int QuestionTypeId { get; set; }
        public virtual QuestionType QuestionType { get; set; }

        public virtual ICollection<QuestionAnswer> Answers { get; set; }
        public virtual ICollection<QuestionAnswerMatch> AnswerMatches { get; set; }
    }
}

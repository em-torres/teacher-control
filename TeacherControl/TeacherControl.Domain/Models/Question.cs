﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Points { get; set; }
        public bool IsRequired { get; set; }

        public int QuestionnaireSectionId { get; set; }
        public virtual QuestionnaireSection QuestionnaireSection { get; set; }
        public virtual IEnumerable<QuestionAnswer> Answers { get; set; }
    }
}

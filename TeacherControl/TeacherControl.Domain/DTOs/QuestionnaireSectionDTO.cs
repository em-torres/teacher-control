﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherControl.Domain.DTOs
{
    public class QuestionnaireSectionDTO
    {
        public IEnumerable<QuestionDTO> Questions { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.Enums;

namespace TeacherControl.Core.DTOs
{
    public class AssignmentCommentDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int Upvote { get; set; }
        public int Author { get; set; }
        public int Course { get; set; }
    }
}
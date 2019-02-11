using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Domain.Models;

namespace TeacherControl.Domain.DTOs
{
    public class AssignmentStudentPointsDTO
    {
        public int Points { get; set; }
        public Assignment Assignment { get; set; }
        public object Student { get; set; }
    }
}

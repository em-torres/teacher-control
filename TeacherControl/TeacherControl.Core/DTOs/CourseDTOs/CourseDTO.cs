using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TeacherControl.Core.Enums;

namespace TeacherControl.Core.DTOs
{
    public class CourseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Credits { get; set; }
        public string CodeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Professor { get; set; }
        public ICollection<string> Tags { get; set; }
        public ICollection<CourseStudentDTO> Students { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace TeacherControl.Domain.Models
{
    public class AssignmentStudentPoint : BaseModel
    {
        public float Points { get; set; }
        public virtual Assignment Assignment { get; set; }
        public virtual User Student { get; set; }
    }
}

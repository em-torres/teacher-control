using System.ComponentModel.DataAnnotations;

namespace TeacherControl.Domain.Models
{
    public class AssignmentStudentPoint : BaseModel
    {
        public float Points { get; set; }
        public virtual Assignment Assignment { get; set; }
        public virtual User Student { get; set; }

        public AssignmentStudentPoint()
        {
            Points = 0;
            Assignment = new Assignment();
            Student = new User();
        }

        public AssignmentStudentPoint(float points, Assignment assignment, User student)
        {
            Points = points;
            Assignment = assignment;
            Student = student;
        }
    }
}

namespace TeacherControl.Domain.Models
{
    public class AssignmentTag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}

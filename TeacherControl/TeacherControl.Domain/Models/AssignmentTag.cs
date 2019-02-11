namespace TeacherControl.Domain.Models
{
    public class AssignmentTag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        public AssignmentTag()
        {
            Id = 0;
            Name = string.Empty;
            AssignmentId = 0;
        }

        public AssignmentTag(int Id, string name, Assignment assignment)
        {
            this.Id = Id;
            Name = name;
            AssignmentId = assignment.Id;
            Assignment = assignment;
        }
    }
}

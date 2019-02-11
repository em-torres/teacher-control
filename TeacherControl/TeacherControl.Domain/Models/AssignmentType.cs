namespace TeacherControl.Domain.Models
{
    public class AssignmentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        public AssignmentType(int Id, string name, string description, Assignment assignment)
        {
            this.Id = Id;
            Name = name;
            Description = description;
            Assignment = assignment;
            AssignmentId = assignment.Id;
        }

        public AssignmentType()
        {
            this.Id = Id;
            Name = string.Empty;
            Description = string.Empty;
            Assignment = new Assignment();
            AssignmentId = 0;
        }
    }
}

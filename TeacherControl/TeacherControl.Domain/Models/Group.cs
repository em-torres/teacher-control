using System.Collections.Generic;

namespace TeacherControl.Domain.Models
{
    public class Group : BaseModel
    {
        public string Name { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<GroupPrivilege> Privileges { get; set; }
        public virtual ICollection<UserGroup> Users { get; set; }
        public virtual ICollection<AssignmentGroup> Assignments { get; set; }

        public Group()
        {
            Id = 0;
            Name = string.Empty;
            Privileges = new HashSet<GroupPrivilege>();
            Users = new HashSet<UserGroup>();
            Assignments = new HashSet<AssignmentGroup>();
            Status = new Status();
            StatusId = 0;
        }

        public Group(int Id, string name, Status status)
        {
            this.Id = Id;
            Name = name;
            Status = status;
            Privileges = new HashSet<GroupPrivilege>();
            Users = new HashSet<UserGroup>();
            Assignments = new HashSet<AssignmentGroup>();
        }
    }
}

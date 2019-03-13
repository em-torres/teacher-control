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
    }
}

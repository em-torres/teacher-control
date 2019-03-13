using System;
using System.Collections.Generic;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class Group : IModificationAudit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<GroupPrivilege> Privileges { get; set; }
        public virtual ICollection<UserGroup> Users { get; set; }
        public virtual ICollection<AssignmentGroup> Assignments { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

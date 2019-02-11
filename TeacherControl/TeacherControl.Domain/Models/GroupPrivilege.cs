using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class GroupPrivilege : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}

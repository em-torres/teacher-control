using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.Models
{
    public class GroupPrivilege 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}

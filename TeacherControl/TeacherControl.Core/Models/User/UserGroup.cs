using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Core.Models
{
    public class UserGroup
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Group Group { get; set; }

        public int UserId { get; set; }
        public int GroupId { get; set; }
    }
}

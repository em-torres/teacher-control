using System;
using System.Collections.Generic;

namespace TeacherControl.Domain.DTOs
{
    public class GroupDTO
    {
        public string Name { get; set; }
        public int Status { get; set; }

        public IEnumerable<string> Privileges { get; set; }
    }
}

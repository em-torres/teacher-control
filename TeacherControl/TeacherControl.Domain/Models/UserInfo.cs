using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherControl.Domain.Models
{
    public class UserInfo : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserInfo(string name, string lastName)
        {
            FirstName = name;
            LastName = lastName;
        }

        public UserInfo()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
        }
    }
}

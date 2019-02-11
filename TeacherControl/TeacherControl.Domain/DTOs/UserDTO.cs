using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherControl.Domain.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public virtual UserInfoDTO UserInfo { get; set; }
        public virtual ICollection<string> Groups { get; set; }
    }
}

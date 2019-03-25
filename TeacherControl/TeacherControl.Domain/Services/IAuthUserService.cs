using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.Services
{
    public interface IAuthUserService
    {
        string Username { get; set; }
    }
}

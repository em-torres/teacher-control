using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        int Add(UserCredentialsDTO dto);
        User Authenticate(string Username, string Password);
    }
}

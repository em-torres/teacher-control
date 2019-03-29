using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Enums;
using TeacherControl.Core.Models;
using TeacherControl.Core.Queries;

namespace TeacherControl.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<UserInfoDTO> GetAll(UserQuery query);
        int Add(UserCredentialDTO dto);
        User Authenticate(string Username, string Password);
        int Remove(int UserId);
        int UpdateCredentials(int UserId, UserCredentialDTO dto);
        int AddUserInfo(int userId, UserInfoDTO dto);
        int UpdateUserInfo(int UserId, UserInfoDTO dto);
        int AddUserRoles(int UserId, IEnumerable<string> roles);
        int UpdateUserRoles(int UserId, IEnumerable<string> roles);
        int RemoveUserRoles(int UserId, IEnumerable<string> roles);
        IEnumerable<CourseCreditsDTO> GetCourseCredits(int UserId);
        CourseCreditsDTO GetCourseCreditsByCourseId(int UserId, int courseId);
        bool IsUsernameAvailable(string Username);
        bool IsEmailAvailable(string Email);
    }
}

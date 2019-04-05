using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TeacherControl.Common.Enums;
using TeacherControl.Common.Helpers;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Core.Queries;
using TeacherControl.Domain.Repositories;
using TeacherControl.DataEFCore.Extensors;
using TeacherControl.Common.Extensors;

namespace TeacherControl.DataEFCore.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TCContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<UserInfoDTO> GetAll(UserQuery query)
        {
            var list = GetAll();

            IQueryable<UserInfo> users = list
                .GetByPhoneNumber(query.Username)
                .GetByGender(query.Gender)
                .GetByCodeId(query.CodeId)
                .GetByEmail(query.Email)
                .GetByBirthday(query.FromBirthday, query.FromBirthday)
                .Pagination(query.Page, query.PageSize)
                .Where(i => i.UserInfo != null) //this is because admins or modders not neccessary needs extra infos
                .Select(i => i.UserInfo);

            return _Mapper.Map<IEnumerable<UserInfo>, IEnumerable<UserInfoDTO>>(users);
        }

        public int Add(UserCredentialDTO dto)
        {
            User user = _Mapper.Map<UserCredentialDTO, User>(dto);
            user.SaltToken = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            user.Password = CredentialHelper.CreatePasswordHash(user.Password, user.SaltToken);

            return Add(user);
        }

        public User Authenticate(string Username, string Password)
        {
            User user = null;

            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                user = Find(i => i.Username.Equals(Username));
                if (user is null)
                    return user;

                if (!CredentialHelper.VerifyPasswordHash(Password, user.Password, user.SaltToken)) return null;

                return user;
            }

            return user;
        }

        public int Remove(int UserId)
        {
            User user = Find(i => i.Id.Equals(UserId));

            if (user is null || user.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            return Remove(user);
        }

        public int UpdateCredentials(int UserId, UserCredentialDTO dto)
        {
            User user = Find(i => i.Id.Equals(UserId));

            if (user is null || user.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            return Update(user, dto);
        }

        public int AddUserInfo(int userId, UserInfoDTO dto)
        {
            User user = Find(i => i.Id.Equals(userId));

            if (user is null || user.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            if (user.UserInfo != null && user.UserInfo.Id > 0)
            {
                return (int)TransactionStatus.UNCHANGED;
            }

            UserInfo UserInfo = _Mapper.Map<UserInfoDTO, UserInfo>(dto);
            UserInfo.UserId = user.Id;

            _Context.UserInfos.Add(UserInfo);
            return _Context.SaveChanges();
        }

        public int UpdateUserInfo(int UserId, UserInfoDTO dto)
        {
            User user = Find(i => i.Id.Equals(UserId));

            if (user is null || user.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            if (user.UserInfo != null && user.UserInfo.Id > 0)
            {
                user.UserInfo = _Mapper.Map<UserInfoDTO, UserInfo>(dto);
                return _Context.SaveChanges();
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }

        public int AddUserRoles(int UserId, IEnumerable<string> roles)
        {
            User user = Find(i => i.Id.Equals(UserId));

            if (user is null || user.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            roles.ToList().ForEach(i =>
            {
                if (!user.Roles.Any(m => m.Role.Name.ToLower().Equals(i.ToLower())))
                {
                    user.Roles.Add(new UserRole { User = user, Role = new Role { Name = i } });
                }
            });

            return _Context.SaveChanges();
        }

        public int UpdateUserRoles(int UserId, IEnumerable<string> roles)
        {
            User user = Find(i => i.Id.Equals(UserId));

            if (user is null || user.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            _Context.UserRoles.RemoveRange(user.Roles);
            roles.ToList().ForEach(i =>
                    user.Roles.Add(new UserRole { User = user, Role = new Role { Name = i } }));

            return _Context.SaveChanges();
        }

        public int RemoveUserRoles(int UserId, IEnumerable<string> roles)
        {
            User user = Find(i => i.Id.Equals(UserId));

            if (user is null || user.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            _Context.UserRoles.RemoveRange(user.Roles);
            return _Context.SaveChanges();
        }

        public IEnumerable<CourseCreditsDTO> GetCourseCredits(int UserId)
        {
            User user = Find(i => i.Id.Equals(UserId));
            if (user is null || user.Id > 0) return new List<CourseCreditsDTO>();

            return _Mapper.Map<User, IEnumerable<CourseCreditsDTO>>(user);
        }

        public CourseCreditsDTO GetCourseCreditsByCourseId(int UserId, int courseId)
        {
            User user = Find(i => i.Id.Equals(UserId));
            Course course = _Context.Courses.Where(i => i.Id.Equals(courseId)).FirstOrDefault();

            if (user is null || user.Id > 0) return new CourseCreditsDTO();
            if (course is null || course.Id > 0) return new CourseCreditsDTO();

            return _Mapper.Map<User, CourseCreditsDTO>(user);
        }

        public bool IsUsernameAvailable(string Username)
        {
            string username = Username.ToLower();
            return _Context.Users.Any(i => i.Username.ToLower().Equals(username));
        }

        public bool IsEmailAvailable(string Email)
        {
            string email = Email.ToLower();
            return _Context.Users.Any(i => i.Email.ToLower().Equals(email));
        }
    }
}

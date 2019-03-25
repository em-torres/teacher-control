using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TeacherControl.Common.Helpers;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Domain.Repositories;
using TeacherControl.Domain.Services;

namespace TeacherControl.DataEFCore.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TCContext context,  IMapper mapper) : base(context, mapper)
        {
        }

        public int Add(UserCredentialsDTO dto)
        {
            User user = _Mapper.Map<UserCredentialsDTO, User>(dto);
            user.SaltToken = Guid.NewGuid().ToString().Replace("-", "");
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
    }
}

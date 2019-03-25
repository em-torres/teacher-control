using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.AutoMapperProfiles.UserProfiles
{
    public class UserCredentialsProfile : Profile
    {
        public UserCredentialsProfile()
        {
            CreateMap<User, UserCredentialsDTO>();

            CreateMap<UserCredentialsDTO, User>();
        }
    }
}

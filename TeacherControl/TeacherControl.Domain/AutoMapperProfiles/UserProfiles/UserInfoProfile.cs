using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.AutoMapperProfiles.UserProfiles
{
    public class UserInfoProfile : Profile
    {
        public UserInfoProfile()
        {
            CreateMap<UserInfo, UserInfoDTO>()
                .ForMember(i => i.Email, i => i.MapFrom(m => m.User.Email))
                .ForMember(i => i.Roles, i => i.MapFrom(m => m.User.Roles.Select(r => r.Role.Name)));

            CreateMap<UserInfoDTO, UserInfo>();
        }
    }
}

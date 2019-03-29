using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.AutoMapperProfiles
{
    public class CourseCreditsProfile : Profile
    {
        public CourseCreditsProfile()
        {
            CreateMap<CourseUserCredit, CourseCreditsDTO>()
                .ForMember(i => i.Title, i => i.MapFrom(m => m.Course.Name))
                .ForMember(i => i.CodeId, i => i.MapFrom(m => m.Course.CodeId))
                .ForMember(i => i.Credit, i => i.MapFrom(m => m.Course.Credits));
        }
    }
}

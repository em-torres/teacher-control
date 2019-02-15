using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;

namespace TeacherControl.Domain.AutoMapperProfiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDTO>()
                .ForMember(i => i.Status, i => i.MapFrom(src => src.Status.Id))
                .ForMember(i => i.Tags, i => i.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(i => i.Professor, i => i.MapFrom(src => src.Professor.Id));

            CreateMap<CourseDTO, Course>()
                .ForPath(i => i.Status.Id, i => i.MapFrom(src => src.Status))
                .ForMember(i => i.Tags, i => i.Ignore())
                .ForPath(i => i.Professor.Id, i => i.MapFrom(m => m.Professor));
        }
    }
}

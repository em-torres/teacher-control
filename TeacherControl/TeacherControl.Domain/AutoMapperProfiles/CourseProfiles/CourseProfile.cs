using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.AutoMapperProfiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDTO>()
                .ForPath(i => i.Professor, i => i.Ignore())
                .ForMember(i => i.Status, i => i.MapFrom(src => src.Status.Id))
                .ForMember(i => i.Tags, i => i.MapFrom(src => src.Tags.Select(t => t.Name)));

            CreateMap<CourseDTO, Course>()
                .ForPath(i => i.Professors, i => i.Ignore())
                .ForPath(i => i.Status.Id, i => i.MapFrom(src => src.Status))
                .ForMember(i => i.Tags, i => i.MapFrom(m => m.Tags.Select(t => new CourseTag { Name = t })));
        }
    }
}

using System;
using AutoMapper;
using System.Linq;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.AutoMapperProfiles
{
    public class AssignmentProfile : Profile
    {
        public AssignmentProfile()
        {
            CreateMap<Assignment, AssignmentDTO>()
                .ForMember(i => i.Tags, i => i.MapFrom(src => src.Tags.Select(t => t.Name)));

            CreateMap<AssignmentDTO, Assignment>()
                .ForMember(i => i.Tags, i => i.Ignore());
        }
    }
}

using System;
using AutoMapper;
using System.Linq;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;

namespace TeacherControl.Domain.AutoMapperProfiles
{
    public class AssignmentProfile : Profile
    {
        public AssignmentProfile()
        {
            CreateMap<Assignment, AssignmentDTO>()
                .ForMember(i => i.Status, i => i.MapFrom(src => src.Status.Id))
                .ForMember(i => i.Tags, i => i.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(i => i.Types, i => i.MapFrom(src => src.Types.Select(t => t.Name)))
                .ForMember(i => i.Groups, i => i.MapFrom(src => src.Groups.Select(t => t.Group.Name)));

            CreateMap<AssignmentDTO, Assignment>()
                .ForPath(i => i.Status.Id, i => i.MapFrom(src => src.Status))
                .ForMember(i => i.Tags, i => i.Ignore())
                .ForMember(i => i.Types, i => i.Ignore())
                .ForMember(i => i.Groups, i => i.Ignore());
        }
    }
}

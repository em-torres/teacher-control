using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.AutoMapperProfiles.CourseProfiles
{
    public class CourseCommentProfile : Profile
    {
        public CourseCommentProfile()
        {
            CreateMap<AssignmentComment, AssignmentCommentDTO>()
                .ForMember(i => i.Author, i => i.MapFrom(m => m.AuthorId))
                .ForMember(i => i.Course, i => i.MapFrom(m => m.AssignmentId));

            CreateMap<AssignmentCommentDTO, AssignmentComment>()
                .ForPath(i => i.Author.Id, i => i.MapFrom(m => m.Author))
                .ForPath(i => i.Assignment.Id, i => i.MapFrom(m => m.Course));
        }
    }
}

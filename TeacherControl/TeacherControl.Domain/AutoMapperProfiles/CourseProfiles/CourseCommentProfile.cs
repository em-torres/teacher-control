using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .ForMember(i => i.Upvote, i => i.MapFrom(m => m.Upvotes.Count()))
                .ForMember(i => i.Downvote, i => i.MapFrom(m => m.Downvotes.Count()));

            //CreateMap<AssignmentCommentDTO, AssignmentComment>();
        }
    }
}

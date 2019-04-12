using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.DTOs;

namespace TeacherControl.Domain.AutoMapperProfiles.AssignmentProfiles
{
    public class AssignmentCommentAddProfile : Profile
    {
        public AssignmentCommentAddProfile()
        {
            CreateMap<AssignmentCommentAddDTO, AssignmentCommentDTO>();
        }
    }
}

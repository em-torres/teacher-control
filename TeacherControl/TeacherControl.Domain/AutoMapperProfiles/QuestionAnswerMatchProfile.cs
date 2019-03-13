using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.AutoMapperProfiles
{
    public class QuestionAnswerMatchProfile : Profile
    {
        public QuestionAnswerMatchProfile()
        {
            CreateMap<UserAnswerMatch, QuestionAnswerMatchDTO>();

            CreateMap<QuestionAnswerMatch, QuestionAnswerMatchDTO>();
        }

    }
}

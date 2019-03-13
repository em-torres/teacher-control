using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;

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

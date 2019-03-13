using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.AutoMapperProfiles
{
    public class QuestionAnswerProfile : Profile
    {
        public QuestionAnswerProfile()
        {
            CreateMap<QuestionAnswerDTO, QuestionAnswer>()
                .ForMember(i => i.Question, i => i.Ignore());
        }
    }
}

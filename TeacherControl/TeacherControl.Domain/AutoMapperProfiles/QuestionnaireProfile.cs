using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.AutoMapperProfiles
{
    public class QuestionnaireProfile : Profile
    {
        public QuestionnaireProfile()
        {
            CreateMap<Questionnaire, QuestionnaireDTO>()
                .ForMember(i => i.Status, i => i.MapFrom(src => src.Status.Id));

            CreateMap<QuestionnaireDTO, Questionnaire>()
                .ForPath(i => i.Status.Id, i => i.MapFrom(src => src.Status))
                .ForMember(i => i.Assignment, i => i.Ignore());
        }
    }
}

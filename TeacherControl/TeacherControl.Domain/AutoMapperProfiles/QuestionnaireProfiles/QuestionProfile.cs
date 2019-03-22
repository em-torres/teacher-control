using AutoMapper;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;

namespace TeacherControl.Domain.AutoMapperProfiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionDTO, Question>()
                .ForMember(i => i.AnswerMatches, i => i.Ignore())
                .ForMember(i => i.Questionnaire, i => i.Ignore());
        }
    }
}

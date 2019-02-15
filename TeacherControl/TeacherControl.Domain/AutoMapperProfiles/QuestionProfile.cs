using AutoMapper;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;

namespace TeacherControl.Domain.AutoMapperProfiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionDTO, Question>()
                .ForMember(i => i.AnswerMatch, i => i.Ignore())
                .ForMember(i => i.Questionnaire, i => i.Ignore());
        }
    }
}

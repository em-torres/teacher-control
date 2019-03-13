using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;

namespace TeacherControl.Domain.AutoMapperProfiles
{
    public class AssignmentResultProfile : Profile
    {
        public AssignmentResultProfile()
        {
            CreateMap<Assignment, AssignmentResultDTO>()
                .ForMember(i => i.PointsToPass, i => i.MapFrom(m => m.Points))
                .ForMember(i => i.TotalUserPoints, i => i.Ignore())
                .ForMember(i => i.IsPassed, i => i.Ignore());
        }

        protected double GetTotalUserPoints(IEnumerable<Questionnaire> questionnaires)
        {
            double result = 0;

            //result = questionnaires.SelectMany(i => i.Questions).Sum(i => i.Answers);
            return result;
        }
    }
}

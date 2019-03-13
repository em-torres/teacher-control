using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;
using TeacherControl.Domain.Queries;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.DataEFCore.Repositories
{
    public class QuestionnaireRepository : BaseRepository<Questionnaire>, IQuestionnaireRepository
    {

        public QuestionnaireRepository(TCContext Context, IMapper Mapper) : base(Context, Mapper)
        {
        }

        public int Add(int AssignmentID, QuestionnaireDTO dto, string createdBy)
        {
            Questionnaire entity = _Mapper.Map<QuestionnaireDTO, Questionnaire>(dto);
            entity.AssignmentId = AssignmentID;
            entity.CreatedBy = createdBy;
            entity.Status = _Context.Statuses.Where(i => i.Id.Equals((int)Domain.Enums.Status.Active)).First();

            return Add(entity);
        }

        public IEnumerable<QuestionAnswerDTO> GetCorrectQuestionAnswers(int AssignmentID, int questionnaireID)
        {
            Questionnaire questionnaire = _Context.Assignments
                .Where(i => i.Id.Equals(AssignmentID))
                .SelectMany(i => i.Questionnaires)
                .Where(i => i.Id.Equals(questionnaireID)).First();

            IEnumerable<QuestionAnswer> questionAnswers = questionnaire.Questions
                .SelectMany(i => i.Answers)
                .Where(i => i.IsCorrect.Equals(true));

            return _Mapper.Map<IEnumerable<QuestionAnswer>, IEnumerable<QuestionAnswerDTO>>(questionAnswers);
        }

        public IEnumerable<QuestionDTO> GetQuestions(int AssignmentID, int questionnaireID)
        {
            Questionnaire questionnaire = _Context.Assignments
                .Where(i => i.Id.Equals(AssignmentID))
                .SelectMany(i => i.Questionnaires)
                .Where(i => i.Id.Equals(questionnaireID)).First();

            return _Mapper.Map<IEnumerable<Question>, IEnumerable<QuestionDTO>>(questionnaire.Questions);
        }

        public IEnumerable<QuestionAnswerMatchDTO> GetQuestionAnswerMatches(int AssignmentID, int questionnaireID)
        {
            Questionnaire questionnaire = _Context.Assignments
               .Where(i => i.Id.Equals(AssignmentID))
               .SelectMany(i => i.Questionnaires)
               .Where(i => i.Id.Equals(questionnaireID)).First();

            IEnumerable<QuestionAnswerMatch> matches = questionnaire.Questions.SelectMany(i => i.AnswerMatches);

            return _Mapper.Map<IEnumerable<QuestionAnswerMatch>, IEnumerable<QuestionAnswerMatchDTO>>(matches);
        }

        public Task<IEnumerable<QuestionnaireDTO>> GetAllQuestionnaires(int AssignmentID)
        {
            IQueryable<Questionnaire> dTOs = _Context.Questionnaires.Where(i => i.AssignmentId.Equals(AssignmentID));
            return Task.Run(() => _Mapper.Map<IQueryable<Questionnaire>, IEnumerable<QuestionnaireDTO>>(dTOs));
        }

    }
}

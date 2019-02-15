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

        //public Task<IEnumerable<QuestionnaireDTO>> GetByFilters(QuestionnaireQuery query)
        //{
        //    IQueryable<Questionnaire> dTOs = _Context.Questionnaires.Where(i => i.AssignmentId.Equals(AssignmentID));



        //    return Task.Run(() => _Mapper.Map<IQueryable<Questionnaire>, IEnumerable<QuestionnaireDTO>>(dTOs));
        //}

        //public IEnumerable<QuestionMatchDTO> GetQuestionMatches(int questionnaireID)
        //{
        //    IEnumerable<QuestionAnswerMatch> matches = _Context.QuestionAnswerMatches
        //        .Where(i => i.LeftQuestionAnswer.Question.QuestionnaireId.Equals(questionnaireID) &&
        //        i.RightQuestionAnswer.Question.QuestionnaireId.Equals(questionnaireID));

        //    return _Mapper.Map<IEnumerable<QuestionAnswerMatch>, IEnumerable<QuestionAnswerMatchDTO>>(matches);
        //}

        //public IEnumerable<QuestionMatchAnswerDTO> GetUserQuestionMatchAnswers(int questionnaireID, int userID)
        //{
        //    IEnumerable<QuestionAnswerUserMatch> matches = _Context.QuestionAnswerUserMatches
        //        .Where(i => i.User.Id.Equals(userID))
        //        .Where(i => i.LeftQuestionAnswer.Question.QuestionnaireId.Equals(questionnaireID) &&
        //            i.RightQuestionAnswer.Question.QuestionnaireId.Equals(questionnaireID));

        //    return _Mapper.Map<IEnumerable<QuestionAnswerUserMatch>, IEnumerable<QuestionAnswerUserMatchDTO>>(matches);
        //}

        //public IEnumerable<QuestionDTO> GetQuestions(int questionnaireID)
        //{
        //    IEnumerable<Question> questions =
        //        _Context.Questions.Where(i => i.QuestionnaireId.Equals(questionnaireID));

        //    return _Mapper.Map<IEnumerable<Question>, IEnumerable<QuestionDTO>>(questions);
        //}

        //public IEnumerable<QuestionAnswerUserDTO> GetUserQuestionAnswers(int questionnaireID)
        //{
        //    IEnumerable<QuestionAnswer> questionAnswers =
        //        _Context.QuestionAnswers.Where(i => i.Question.QuestionnaireId.Equals(questionnaireID));

        //    return _Mapper.Map<IEnumerable<QuestionAnswerUser>, IEnumerable<QuestionAnswerUserDTO>>(questionAnswers);
        //}

    }
}

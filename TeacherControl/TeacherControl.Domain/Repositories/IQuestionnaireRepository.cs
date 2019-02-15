using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;
using TeacherControl.Domain.Queries;

namespace TeacherControl.Domain.Repositories
{
    public interface IQuestionnaireRepository : IRepository<Questionnaire>
    {
        int Add(int AssignmentID, QuestionnaireDTO dto, string createdBy);
        //Task<IEnumerable<QuestionnaireDTO>> GetByFilters(int AssignmentID, QuestionnaireQuery query);

        //IEnumerable<QuestionMatchDTO> GetQuestionMatches(int questionnaireID);
        //IEnumerable<QuestionMatchAnswerDTO> GetUserQuestionMatchAnswers(int questionnaireID, int userID);
        //IEnumerable<QuestionDTO> GetQuestions(int questionnaireID);
        //IEnumerable<UserQuestionAnswerDTO> GetUserQuestionAnswers(int questionnaireID);
    }
}

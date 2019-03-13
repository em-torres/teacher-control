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
        IEnumerable<QuestionAnswerDTO> GetCorrectQuestionAnswers(int AssignmentID, int questionnaireID);
        IEnumerable<QuestionDTO> GetQuestions(int AssignmentID, int questionnaireID);
        IEnumerable<QuestionAnswerMatchDTO> GetQuestionAnswerMatches(int AssignmentID, int questionnaireID);
        Task<IEnumerable<QuestionnaireDTO>> GetAllQuestionnaires(int AssignmentID);
    }
}

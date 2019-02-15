using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TeacherControl.API.Extensors;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Queries;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Controllers
{
    [Route("api/assignments/{assignmentId:int:min(1)}/questionnaires")]
    public class QuestionnaireController : Controller
    {
        protected readonly IQuestionnaireRepository _QuestionnaireRepo;

        public QuestionnaireController(IQuestionnaireRepository questionnaireRepo)
        {
            _QuestionnaireRepo = questionnaireRepo;
        }

        //[HttpGet]
        //public IActionResult GetAll([FromRoute] int assignmentId, [FromQuery] QuestionnaireQuery query)
        //{
        //    return this.Ok(() => JArray.FromObject(_QuestionnaireRepo.GetByFilters(query).Result));
        //}


        [HttpPost]
        public IActionResult AddQuestionnaire([FromRoute] int assignmentId, [FromBody] QuestionnaireDTO dto)
        {
            string username = this.GetUsername();
            return this.NoContent(() => _QuestionnaireRepo.Add(assignmentId, dto, "Test") > 0);
        }

        //[HttpGet, Route("{questionnaireID:int:min(1)}")]
        //public IActionResult GetAllAnswers(int questionnaireID)
        //{
        //    return this.Ok(() => JArray.FromObject(_QuestionnaireRepo.GetAllAnswers(assignmentID, questionnaireID)));
        //}

        //[HttpGet, Route("{questionnaireID:int:min(1)}/questions")]
        //public IActionResult GetAllQuestions(int assignmentID, int questionnaireID)
        //{
        //    return this.Ok(() => JArray.FromObject(_QuestionnaireRepo.GetAllQuestions(assignmentID, questionnaireID)));
        //}

        //[HttpGet, Route("{questionnaireID:int:min(1)}/question-matches")]
        //public IActionResult GetAllQuestionMatches(int assignmentID, int questionnaireID)
        //{
        //    return this.Ok(() => JArray.FromObject(_QuestionnaireRepo.GetAllQuestionMatches(assignmentID, questionnaireID)));
        //}

        //[HttpGet, Route("{questionnaireID:int:min(1)}/question-matches")]
        //public IActionResult GetQuestionnaireResult(int assignmentID, int questionnaireID)
        //{
        //    return this.Ok(() => JArray.FromObject(_QuestionnaireRepo.GetAllQuestionMatches(assignmentID, questionnaireID)));
        //}

        //[HttpGet, Route("{questionnaireID:int:min(1)}/question-matches")]
        // put this on the assignment controller
        //public IActionResult GetAllQuestionnaireResults(int assignmentID, int questionnaireID)
        //{
        //    return this.Ok(() => JArray.FromObject(_QuestionnaireRepo.GetAllQuestionMatches(assignmentID, questionnaireID)));
        //}

    }
}
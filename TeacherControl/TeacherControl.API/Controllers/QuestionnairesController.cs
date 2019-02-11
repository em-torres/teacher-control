using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherControl.Common.Extensors;
using TeacherControl.Common;
using TeacherControl.Core.Interfaces.Repositories;
using TeacherControl.Core.Models;
using TeacherControl.Infrastructure;
using TeacherControl.Infrastructure.Repositories;
using TeacherControl.WebApi.Models;

namespace TeacherControl.WebApi.Controllers
{
    [Route("api/assignments/{id:int:min(1)}/questionnaires")]
    public class QuestionnairesController : Controller
    {
        protected TCContext _TCContext;
        protected IAssignmentRepository _AssignmentRepo;
        protected IGroupRepository _GroupRepo;
        protected IStatusRepository _StatusRepo;

        public QuestionnairesController()
        {
            _TCContext = new TCContext();
            _AssignmentRepo = new AssignmentRepository(_TCContext);
            _GroupRepo = new GroupRepository(_TCContext);
            _StatusRepo = new StatusRepository(_TCContext);
        }

        [HttpGet]
        public IActionResult GetAssignmentQuestionnaires([FromRoute(Name = "id")] int assignmentId)
        {
            return this.Ok(() =>
            {
                JArray json = new JArray();
                if (assignmentId > 0)
                {
                    List<Questionnaire> data = _AssignmentRepo.Find(i => i.Id.Equals(assignmentId)).Questionnaires.ToList();
                    data.ForEach(e =>
                    {
                        JObject questionnaire = new JObject
                        {
                            ["Questionnaire"] = JObject.FromObject(new { e.AssignmentId, e.Title, e.Body }),
                            ["Sections"] = JArray.FromObject(
                                        e.Sections.Select(s => 
                                        {
                                            var questions = s.Questions.Select(q => new { q.Title, q.Points, q.IsRequired,
                                                    Answers = q.Answers.Select(a => new { a.Title, a.MaxLength, a.IsCorrect, }) });

                                            return new { s.Page, Questions = questions };
                                        })),
                        };

                        json.Add(questionnaire);
                    });
                }

                return json.AsQueryable();
            });
        }

        [HttpPost]
        public IActionResult SaveAssignmentQuestionnaires([FromRoute(Name = "id")] int assignmentId, [FromBody] IEnumerable<QuestionnaireViewModel> viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest("Invalid Json body");
            }
            return this.NoContent(() =>
            {
                if (assignmentId > 0)
                {
                    Assignment assignment = _AssignmentRepo.Find(i => i.Id.Equals(assignmentId));
                    assignment.Questionnaires = BuildQuestionnaires(viewModel);

                    using (UnitOfWork unit = new UnitOfWork(_TCContext))
                    {
                        //assignment.CreatedBy = User.Identity.Name;
                        //assignment.UpdatedBy = User.Identity.Name;
                        assignment.CreatedBy = "Test";
                        assignment.UpdatedBy = "Test";

                        _AssignmentRepo.Update(i => i.Id.Equals(assignmentId), assignment);
                        return unit.Commit() > 0;
                    }
                }

                return false;
            });
        }

        private IEnumerable<Questionnaire> BuildQuestionnaires(IEnumerable<QuestionnaireViewModel> list)
        {
            JToken tmp = new JObject();
            List<Questionnaire> questionnaires = new List<Questionnaire>(list.Count());

            foreach (QuestionnaireViewModel viewModel in list)
            {
                Questionnaire questionnaire = new Questionnaire();
                questionnaire.Title = viewModel.Title;
                questionnaire.Body = viewModel.Body;
                questionnaire.Status = _StatusRepo.GetById(viewModel.Status);

                questionnaire.Sections = GetQuestionnaireSections(viewModel.Sections);
                //this.SetModelUserAudit(questionnaire);
                questionnaire.CreatedBy = "test";
                questionnaire.UpdatedBy = "test";

                questionnaires.Add(questionnaire);
            }

            return questionnaires;
        }

        private IEnumerable<QuestionnaireSection> GetQuestionnaireSections(IEnumerable<QuestionnaireSectionViewModel> questionnaires)
        {
            IList<QuestionnaireSection> sections = new List<QuestionnaireSection>(questionnaires.Count());
            for (int i = 0; i < questionnaires.Count(); i++)
            {
                QuestionnaireSection section = new QuestionnaireSection
                {
                    Page = i + 1,
                    Questions = GetQuestionnaireQuestions(questionnaires.ElementAt(i).Questions)
                };

                sections.Add(section);
            }

            return sections;
        }

        private IEnumerable<Question> GetQuestionnaireQuestions(IEnumerable<QuestionViewModel> questions)
        {
            IList<Question> resultList = new List<Question>();
            foreach (QuestionViewModel model in questions)
            {
                Question q = new Question
                {
                    Answers = GetQuestionAnswers(model.Answers),
                    IsRequired = model.IsRequired,
                    Title = model.Title,
                    Points = model.Points
                };
                resultList.Add(q);
            }

            return resultList;
        }

        private IEnumerable<QuestionAnswer> GetQuestionAnswers(IEnumerable<QuestionAnswerViewModel> questionAnswers)
        {
            IList<QuestionAnswer> answers = new List<QuestionAnswer>(questionAnswers.Count());
            foreach (QuestionAnswerViewModel model in questionAnswers)
            {
                QuestionAnswer a = new QuestionAnswer
                {
                    IsCorrect = model.IsCorrect,
                    MaxLength = model.MaxLength,
                    Title = model.Title
                };

                answers.Add(a);
            }

            return answers;
        }
    }
}

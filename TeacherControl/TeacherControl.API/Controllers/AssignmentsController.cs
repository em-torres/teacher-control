using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TeacherControl.API.Extensors;
using TeacherControl.Common.Enums;
using TeacherControl.Common.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Queries;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Controllers
{
    [Authorize]
    [Route("api/assignments")]
    public class AssignmentsController : Controller
    {
        protected readonly IAssignmentRepository _AssignmentRepo;

        public AssignmentsController(IAssignmentRepository assignmentRepo)
        {
            _AssignmentRepo = assignmentRepo;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] AssignmentQuery filters)
        {
            return this.Ok(() =>
            {
                JObject result = new JObject
                {
                    ["filters"] = filters.ToJson(),
                    ["results"] = _AssignmentRepo.GetByFilters(filters).ToJsonArray()
                };

                return result;
            });
        }

        [HttpPost]
        public IActionResult AddAssignment([FromBody] JObject json)
        {
            if (json is null)
            {
                return BadRequest("The Json Body is Empty");
            }

            return this.Created(() =>
            {
                AssignmentDTO dto = json.ToObject<AssignmentDTO>();
                bool result = _AssignmentRepo.Add(dto).Equals((int)TransactionStatus.SUCCESS);

                return dto.ToJson();
            });
        }

        [HttpDelete, Route("{assignmentId:int:min(1)}")]
        public IActionResult DeleteAssignment([FromRoute] int assignmentId)
        {
            return this.NoContent(() => _AssignmentRepo.DeleteById(assignmentId).Equals(TransactionStatus.SUCCESS));
        }

        [HttpDelete, Route("{assignmentId:length(12, 150)}")] //TODO: validate this range thought the DB
        public IActionResult DeleteAssignment([FromRoute] string assignmentId)
        {
            if (Regex.IsMatch(assignmentId.ToLower(), @"([a-f0-9]{12})$"))
            {
                return this.NoContent(() => _AssignmentRepo.DeleteByTokenId(assignmentId).Equals(TransactionStatus.SUCCESS));
            }

            return BadRequest("The Request has an invalid ID");
        }

        [HttpPut, Route("{assignmentId:int:min(1)}")]
        public IActionResult UpdateAssignment([FromQuery] int assignmentId, [FromBody] AssignmentDTO dto)
        {
            return this.NoContent(() => _AssignmentRepo.Update(assignmentId, dto).Equals(TransactionStatus.SUCCESS));
        }

        [HttpPatch, Route("{assignmentId:int:min(1)}/update-tags")]
        public IActionResult UpdateAssigmentTags([FromQuery] int assignmentId, [FromBody] IEnumerable<string> tags)
        {
            if (tags != null && tags.Count() > 0)
            {
                    tags = tags.Where(i => Regex.IsMatch(i, @"[\w\-#]{3,30}"));
                    return this.NoContent(() => _AssignmentRepo.UpdateTags(assignmentId, tags).Equals(TransactionStatus.SUCCESS));
            }

            return BadRequest("The Json Body is invalid");
        }

        //TODO: get if the user successfully complete the assignment
        //[HttpGet, Route("is-passed")]
        //public IActionResult IsPassed([FromRoute] int assignmentId)
        //{
        //    //return this.Ok(() => JArray.FromObject(_AssignmentRepo.GetQuestionnaireResults(assignmentId, this.GetUsername()));
        //    return null;
        //}

        //TODO: get the questionnaire result set

        [HttpGet, Route("{assignmentId:int:min(1)}/comments")]
        public IActionResult GetCourseComments([FromRoute] int courseId, [FromQuery] AssignmentCommentQuery commentQuery)
        {
            return this.Ok(() =>
            {
                IEnumerable<AssignmentCommentDTO> data = _AssignmentRepo.GetAllAssignmentComments(courseId, commentQuery);
                var query = new
                {
                    PageSize = commentQuery.PageSize <= 0 ? 50 : commentQuery.PageSize,
                    Page = commentQuery.Page <= 0 ? 1 : commentQuery.Page
                };


                return new { query, data }.ToJson();
            });
        }

        [HttpPost, Route("{assignmentId:int:min(1)}/comments")]
        public IActionResult AddCourseComment([FromRoute] int courseId, [FromBody] AssignmentCommentDTO dto)
        {
            return this.Created(() => _AssignmentRepo.AddComment(courseId, dto).Equals((int)TransactionStatus.SUCCESS)
                ? dto.ToJson()
                : new JObject());
        }

        [HttpPost, Route("{assignmentId:int:min(1)}/comments/{commentId:int:min(1)}/disable")]
        public IActionResult DisableCourseComment([FromRoute] int courseId, [FromRoute] int commentId)
        {
            int successTransactionValue = (int)TransactionStatus.SUCCESS;

            return this.NoContent(() => _AssignmentRepo.DisableAssignmentComment(courseId, commentId).Equals(successTransactionValue));
        }

        [HttpDelete, Route("{assignmentId:int:min(1)}/comments/{commentId:int:min(1)}")]
        public IActionResult RemoveCourseComment([FromRoute] int courseId, [FromRoute] int commentId)
        {
            int successTransactionValue = (int)TransactionStatus.SUCCESS;

            return this.NoContent(() => _AssignmentRepo.RemoveAssignmentComment(courseId, commentId).Equals(successTransactionValue));
        }

        [HttpPut, Route("{assignmentId:int:min(1)}/comments/{commentId:int:min(1)}")]
        public IActionResult UpdateCourseComment([FromRoute] int courseId, [FromRoute] int commentId, [FromBody] JObject json)
        {
            int successTransactionValue = (int)TransactionStatus.SUCCESS;

            return this.NoContent(() =>
            {
                AssignmentCommentDTO dto = json.ToObject<AssignmentCommentDTO>();
                return _AssignmentRepo.UpdateComment(courseId, commentId, dto).Equals(successTransactionValue);
            });
        }
    }
}
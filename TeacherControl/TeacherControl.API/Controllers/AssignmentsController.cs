using AutoMapper;
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
        protected readonly IMapper _Mapper;

        public AssignmentsController(IAssignmentRepository assignmentRepo, IMapper mapper)
        {
            _AssignmentRepo = assignmentRepo;
            _Mapper = mapper;
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
        public IActionResult AddAssignment([FromBody] AssignmentDTO dto)
        {
            return this.Created(() =>
            {
                bool result = _AssignmentRepo.Add(dto).Equals((int)TransactionStatus.SUCCESS);

                return result ? dto.ToJson() : new JObject();
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
            Match match = new Regex(@"([a-f0-9]{12})$").Match(assignmentId);

            if (match.Success)
            {
                return this.NoContent(() => _AssignmentRepo.DeleteByTokenId(match.Value).Equals(TransactionStatus.SUCCESS));
            }

            return BadRequest("The Request has an invalid ID");
        }

        [HttpPatch, Route("{assignmentId:int:min(1)}/add-tags")]
        public IActionResult AddAssignmentTags([FromRoute] int assignmentId, [FromBody] IEnumerable<string> tags)
        {
            if (tags != null && tags.Count() > 0)
            {
                tags = tags.Where(i => Regex.IsMatch(i, (@"[\w\-#]{3,30}")));

                return this.NoContent(() => _AssignmentRepo.AddTags(assignmentId, tags).Equals(TransactionStatus.SUCCESS));
            }

            return BadRequest("One or More Tags are invalid");
        }

        [HttpPut, Route("{assignmentId:int:min(1)}")]
        public IActionResult UpdateAssignment([FromRoute] int assignmentId, [FromBody] AssignmentDTO dto)
        {
            return this.NoContent(() => _AssignmentRepo.Update(assignmentId, dto).Equals(TransactionStatus.SUCCESS));
        }

        [HttpPatch, Route("{assignmentId:int:min(1)}/update-tags")]
        public IActionResult UpdateAssigmentTags([FromRoute] int assignmentId, [FromBody] IEnumerable<string> tags)
        {
            if (tags != null && tags.Count() > 0)
            {
                tags = tags.Where(i => Regex.IsMatch(i, @"[\w\-#]{3,30}"));
                return this.NoContent(() => _AssignmentRepo.UpdateTags(assignmentId, tags).Equals(TransactionStatus.SUCCESS));
            }

            return BadRequest("One or More Tags are invalid");
        }

        [HttpPatch, Route("{assignmentId:int:min(1)}/replace-tags")]
        public IActionResult ReplaceTags([FromRoute] int assignmentId, [FromBody] IEnumerable<string> tags)
        {
            if (tags != null && tags.Count() > 0)
            {
                tags = tags.Where(i => Regex.IsMatch(i, @"[\w\-#]{3,30}"));
                return this.NoContent(() => _AssignmentRepo.ReplaceTags(assignmentId, tags).Equals(TransactionStatus.SUCCESS));
            }

            return BadRequest("One or More Tags are invalid");
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
        public IActionResult GetAssignmentComments([FromRoute] int assignmentId, [FromQuery] AssignmentCommentQuery commentQuery)
        {
            return this.Ok(() =>
            {
                IEnumerable<AssignmentCommentDTO> data = _AssignmentRepo.GetAllAssignmentComments(assignmentId, commentQuery);
                var query = new
                {
                    PageSize = commentQuery.PageSize <= 0 ? 50 : commentQuery.PageSize,
                    Page = commentQuery.Page <= 0 ? 1 : commentQuery.Page
                };


                return new { query, data }.ToJson();
            });
        }

        [HttpGet, Route("{assignmentId:int:min(1)}/comments/{commentId:int:min(1)}")]
        public IActionResult GetAssignmentCommentByCommentId([FromRoute] int assignmentId, [FromRoute] int commentId)
        {
            return this.Ok(() => _AssignmentRepo.GetAssignmentCommentByCommentId(assignmentId, commentId).ToJson());
        }

        [HttpPost, Route("{assignmentId:int:min(1)}/comments")]
        public IActionResult AddAssignmentComment([FromRoute] int assignmentId, [FromBody] AssignmentCommentAddDTO dto)
        {
            return this.Created(() => {
                AssignmentCommentDTO comment = _Mapper.Map<AssignmentCommentAddDTO, AssignmentCommentDTO>(dto);
                comment.Author = User.Identity.Name;

                return _AssignmentRepo.AddComment(assignmentId, comment).Equals((int)TransactionStatus.SUCCESS)
                    ? dto.ToJson()
                    : new JObject();
            });
        }

        [HttpPost, Route("{assignmentId:int:min(1)}/comments/{commentId:int:min(1)}/disable")]
        public IActionResult DisableAssignmentComment([FromRoute] int assignmentId, [FromRoute] int commentId)
        {
            int successTransactionValue = (int)TransactionStatus.SUCCESS;

            return this.NoContent(() => _AssignmentRepo.DisableAssignmentComment(assignmentId, commentId).Equals(successTransactionValue));
        }

        [HttpDelete, Route("{assignmentId:int:min(1)}/comments/{commentId:int:min(1)}")]
        public IActionResult RemoveAssignmentComment([FromRoute] int assignmentId, [FromRoute] int commentId)
        {
            int successTransactionValue = (int)TransactionStatus.SUCCESS;

            return this.NoContent(() => _AssignmentRepo.RemoveAssignmentComment(assignmentId, commentId).Equals(successTransactionValue));
        }

        [HttpPut, Route("{assignmentId:int:min(1)}/comments/{commentId:int:min(1)}")]
        public IActionResult UpdateCourseComment([FromRoute] int assignmentId, [FromRoute] int commentId, [FromBody] JObject json)
        {
            int successTransactionValue = (int)TransactionStatus.SUCCESS;

            return this.NoContent(() =>
            {
                AssignmentCommentDTO dto = json.ToObject<AssignmentCommentDTO>();
                return _AssignmentRepo.UpdateComment(assignmentId, commentId, dto).Equals(successTransactionValue);
            });
        }

        [HttpPatch, Route("{assignmentId:int:min(1)}/comments/{commentId:int:min(1)}/upvote")]
        public IActionResult UpvoteAssignmentComment([FromRoute] int assignmentId, [FromRoute] int commentId)
        {
            return this.NoContent(() => _AssignmentRepo.UpvoteComment(assignmentId, commentId).Equals((int) TransactionStatus.SUCCESS));
        }

        [HttpPatch, Route("{assignmentId:int:min(1)}/comments/{commentId:int:min(1)}/downvote")]
        public IActionResult DownvoteAssignmentComment([FromRoute] int assignmentId, [FromRoute] int commentId)
        {
            return this.NoContent(() => _AssignmentRepo.DownvoteComment(assignmentId, commentId).Equals((int)TransactionStatus.SUCCESS));
        }
    }
}
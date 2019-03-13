using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TeacherControl.API.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Queries;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Controllers
{
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
            return this.Ok(() => JArray.FromObject(_AssignmentRepo.GetByFilters(filters).Result));
        }

        [HttpPost]
        public IActionResult AddAssignment([FromBody] AssignmentDTO dto)
        {
            JObject json = new JObject();

            if (_AssignmentRepo.Add(dto, this.GetUsername()) > 0)
                json = JObject.FromObject(dto);

            return this.Ok(() => json);
        }

        [HttpDelete, Route("{assignmentId:int:min(1)}")]
        public IActionResult DeleteAssignment([FromRoute] int assignmentId)
        {
            return this.NoContent(() => _AssignmentRepo.DeleteById(assignmentId, this.GetUsername()) > 0);
        }

        [HttpDelete, Route("{assignmentId:length(12, 150)}")] //TODO: validate this range thought the DB
        public IActionResult DeleteAssignment([FromRoute] string assignmentId)
        {
            if (!string.IsNullOrWhiteSpace(assignmentId))
            {
                if (Regex.IsMatch(assignmentId.ToLower(), @"([a-f0-9]{12})$"))
                {
                    return this.NoContent(() => _AssignmentRepo.DeleteByTokenId(assignmentId, this.GetUsername()) > 0);
                }
            }
            return BadRequest("The Request has an invalid ID");
        }

        [HttpPut, Route("{assignmentId:int:min(1)}")]
        public IActionResult UpdateAssignment([FromQuery] int assignmentId, [FromBody] AssignmentDTO dto)
        {
            lock (new object())
            {
                return this.NoContent(() => _AssignmentRepo.Update(assignmentId, dto, this.GetUsername()) > 0);
            }
        }

        [HttpPatch, Route("{assignmentId:int:min(1)}/update-tags")]
        public IActionResult UpdateAssigmentTags([FromQuery] int assignmentId, [FromBody] IEnumerable<string> tags)
        {
            if (tags != null && tags.Count() > 0)
            {
                lock (new object())
                {
                    tags = tags.Where(i => Regex.IsMatch(i, @"[\w\-#]{3,30}"));
                    return this.NoContent(() => _AssignmentRepo.UpdateTags(assignmentId, tags, this.GetUsername()) > 0);
                }

            }

            return BadRequest("The JsonBody is invalid");
        }

        [HttpPatch, Route("{assignmentId:int:min(1)}/update-groups")]
        public IActionResult UpdateAssigmentGroups([FromQuery] int assignmentId, [FromBody] IEnumerable<string> groups)
        {
            if (groups != null && groups.Count() > 0)
            {
                lock (new object())
                {
                    groups = groups.Where(i => Regex.IsMatch(i, @"[\w\-#@]{3,60}"));
                    return this.NoContent(() => _AssignmentRepo.UpdateGroups(assignmentId, groups, this.GetUsername()) > 0);
                }

            }

            return BadRequest("The Id or the JsonBody are invalid");
        }

        [HttpPatch, Route("{assignmentId:int:min(1)}/upvote")]
        public IActionResult AddAssignmentUpvote([FromQuery] int assignmentId)
        {
            int upvote = 1;
            return this.NoContent(() => _AssignmentRepo.UpdateUpvoteCount(assignmentId, upvote) > 0);
        }

        [HttpPatch, Route("{assignmentId:int:min(1)}/downvote")]
        public IActionResult DownAssignmentUpvote([FromQuery] int assignmentId)
        {
            int upvote = -1;
            return this.NoContent(() => _AssignmentRepo.UpdateUpvoteCount(assignmentId, upvote) > 0);
        }

        //TODO: get if the user successfully complete the assignment
        //[HttpGet, Route("is-passed")]
        //public IActionResult IsPassed([FromRoute] int assignmentId)
        //{
        //    //return this.Ok(() => JArray.FromObject(_AssignmentRepo.GetQuestionnaireResults(assignmentId, this.GetUsername()));
        //    return null;
        //}

        //TODO: get the questionnaire result set

    }
}
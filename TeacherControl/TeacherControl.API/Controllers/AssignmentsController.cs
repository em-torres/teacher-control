using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TeacherControl.API.Extensors;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Queries;
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

        [HttpDelete]
        public IActionResult DeleteAssignment([FromQuery(Name = "id")] int id)
        {
            if (id > 0)
            {
                return this.NoContent(() => _AssignmentRepo.DeleteById(id, this.GetUsername()) > 0);
            }

            return BadRequest("The Request has an invalid ID");
        }

        [HttpDelete]
        public IActionResult DeleteAssignment([FromQuery(Name = "id")] string tokenID)
        {
            if (!string.IsNullOrWhiteSpace(tokenID))
            {
                if (Regex.IsMatch(tokenID.ToLower(), @"([a-f0-9]{12})$"))
                {
                    return this.NoContent(() => _AssignmentRepo.DeleteByTokenId(tokenID, this.GetUsername()) > 0);
                }
            }
            return BadRequest("The Request has an invalid ID");
        }

        [HttpPut]
        public IActionResult UpdateAssignment([FromQuery(Name = "id")] int id, [FromBody] AssignmentDTO dto)
        {
            if (id > 0)
            {
                lock (new object())
                {
                    return this.NoContent(() => _AssignmentRepo.Update(id, dto, this.GetUsername()) > 0);
                }

            }

            return BadRequest("The Id or the JsonBody are invalid");
        }

        [HttpPatch, Route("UpdateTags")]
        public IActionResult UpdateAssigmentTags([FromQuery(Name = "id")] int id, [FromBody] IEnumerable<string> tags)
        {
            if (id > 0 && tags != null && tags.Count() > 0)
            {
                lock (new object())
                {
                    tags = tags.Where(i => Regex.IsMatch(i, @"[\w\-#]{3,30}"));
                    return this.NoContent(() => _AssignmentRepo.UpdateTags(id, tags, this.GetUsername()) > 0);
                }

            }

            return BadRequest("The Id or the JsonBody are invalid");
        }

        [HttpPatch, Route("UpdateGroups")]
        public IActionResult UpdateAssigmentGroups([FromQuery(Name = "id")] int id, [FromBody] IEnumerable<string> groups)
        {
            if (id > 0 && groups != null && groups.Count() > 0)
            {
                lock (new object())
                {
                    groups = groups.Where(i => Regex.IsMatch(i, @"[\w\-#@]{3,60}"));
                    return this.NoContent(() => _AssignmentRepo.UpdateGroups(id, groups, this.GetUsername()) > 0);
                }

            }

            return BadRequest("The Id or the JsonBody are invalid");
        }

        [HttpPatch, Route("UpVote")]
        public IActionResult AddAssignmentUpvote([FromQuery(Name = "id")] int Id)
        {
            int upvote = 1;
            return this.NoContent(() => _AssignmentRepo.UpdateUpvoteCount(Id, upvote) > 0);
        }

        [HttpPatch, Route("DownVote")]
        public IActionResult DownAssignmentUpvote([FromQuery(Name = "id")] int Id)
        {
            int upvote = -1;
            return this.NoContent(() => _AssignmentRepo.UpdateUpvoteCount(Id, upvote) > 0);
        }

        [HttpPatch, Route("GetByTitle")]
        public IActionResult GetByTitle([FromQuery(Name = "title")] string title)
        {
            return this.Ok(() => JArray.FromObject(_AssignmentRepo.GetByTitle(title)));
        }
    }
}
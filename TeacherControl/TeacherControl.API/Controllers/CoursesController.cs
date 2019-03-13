using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TeacherControl.API.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Queries;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Controllers
{
    [Route("/api/courses")]
    public class CoursesController : Controller
    {
        protected readonly ICourseRepository _CourseRepo;

        public CoursesController(ICourseRepository courseRepository)
        {
            _CourseRepo = courseRepository;
        }

        [HttpGet]
        public IActionResult GetAllCourses([FromQuery] CourseQuery filtersDto)
        {
            return this.Ok(() =>
            {
                IEnumerable<CourseDTO> data = _CourseRepo.GetAll(filtersDto);
                filtersDto.PageSize = filtersDto.PageSize <= 0 ? 50 : filtersDto.PageSize;
                filtersDto.Page = filtersDto.Page <= 0 ? 1 : filtersDto.Page;
                JObject json = new JObject()
                {
                    ["filters"] = JObject.FromObject(filtersDto),
                    ["results"] = JArray.FromObject(data),
                };

                return json;
            });
        }

        [HttpPost]
        public IActionResult AddCourse([FromBody] JObject json)
        {
            if (json is null)
            {
                return BadRequest("Invalid Request Body");
            }

            CourseDTO dto = json.ToObject<CourseDTO>();
            dto.Status = Core.Enums.Status.Active;

            return this.Created(() =>
                _CourseRepo.Add(dto, this.GetUsername()) > 0
                    ? JObject.FromObject(dto)
                    : new JObject());
        }

        [HttpDelete, Route("{courseId:int:min(1)}")]
        public IActionResult DeleteCourse([FromRoute] int courseId)
        {
            return this.NoContent(() => _CourseRepo.Remove(courseId, this.GetUsername()) > 0);
        }

        [HttpPut, Route("{courseId:int:min(1)}")]
        public IActionResult UpdateCourse([FromRoute] int courseId, [FromBody] JObject json)
        {
            CourseDTO dto = json.ToObject<CourseDTO>();

            return this.NoContent(() => _CourseRepo.Update(courseId, dto, "test") > 0);
        }

        [HttpGet, Route("{courseId:int:min(1)}/comments")]
        public IActionResult GetCourseComments([FromRoute] int courseId, [FromQuery] CourseCommentQuery query)
        {
            return this.Ok(() => {
                IEnumerable<CourseCommentDTO> data = _CourseRepo.GetAllCourseComments(courseId, query);
                query.PageSize = query.PageSize <= 0 ? 50 : query.PageSize;
                query.Page = query.Page <= 0 ? 1 : query.Page;

                return JObject.FromObject(new { query, data });                
            });
        }

        [HttpPost, Route("{courseId:int:min(1)}/comments")]
        public IActionResult AddCourseComment([FromRoute] int courseId, [FromBody] CourseCommentDTO dto)
        {
            return this.Created(() =>
                _CourseRepo.AddComment(courseId, dto, this.GetUsername()) > 0
                ? JObject.FromObject(dto)
                : new JObject());
        }

        [HttpPost, Route("{courseId:int:min(1)}/comments/{commentId:int:min(1)}")]
        public IActionResult DisableCourseComment([FromRoute] int courseId, [FromRoute] int commentId)
        {
            return this.NoContent(() => _CourseRepo.DisableCourseComment(courseId, commentId) > 0);
        }

        [HttpPut, Route("{courseId:int:min(1)}/comments/{commentId:int:min(1)}")]
        public IActionResult UpdateCourseComment([FromRoute] int courseId, [FromRoute] int commentId, [FromBody] CourseCommentDTO dto)
        {
            return this.NoContent(() => _CourseRepo.UpdateComment(courseId, dto, this.GetUsername()) > 0);
        }


        [HttpPatch, Route("{courseId:int:min(1)}/comments/{commentId:int:min(1)}")]
        public IActionResult UpVoteCourseComments([FromRoute] int courseId, [FromRoute] int commentId)
        {
            int upvote = 1;
            return this.NoContent(() => _CourseRepo.UpdateUpvoteCourseComment(courseId, commentId, upvote) > 0);
        }

        [HttpPatch, Route("{courseId:int:min(1)}/comments/{commentId:int:min(1)}")]
        public IActionResult DownVoteCourseComments([FromRoute] int courseId, [FromRoute] int commentId)
        {
            int downvote = -1;
            return this.NoContent(() => _CourseRepo.UpdateUpvoteCourseComment(courseId, commentId, downvote) > 0);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TeacherControl.API.Extensors;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Queries;
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
            return this.Ok(JArray.FromObject(_CourseRepo.GetAll(filtersDto)));
        }

        [HttpPost]
        public IActionResult AddCourse([FromBody] CourseDTO dto)
        {
            return this.Created(() =>
                _CourseRepo.Add(dto, this.GetUsername()) > 0
                    ? JObject.FromObject(dto)
                    : new JObject());
        }

        [HttpDelete]
        public IActionResult DeleteCourse([FromQuery(Name = "id")] int courseId, [FromBody] CourseDTO dto)
        {
            return this.NoContent(() => _CourseRepo.Remove(courseId, dto, this.GetUsername()) > 0);
        }

        [HttpPut]
        public IActionResult UpdateCourse([FromQuery(Name = "id")] int courseId, [FromBody] CourseDTO dto)
        {
            return this.NoContent(() => _CourseRepo.Update(courseId, dto, this.GetUsername()) > 0);
        }

        [HttpGet, Route("{courseId:int:min(1)}/comments")]
        public IActionResult GetCourseComments([FromRoute] int courseId, [FromQuery] CourseCommentQuery query)
        {
            return this.Ok(() => JArray.FromObject(_CourseRepo.GetAllCourseComments(courseId, query)));
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
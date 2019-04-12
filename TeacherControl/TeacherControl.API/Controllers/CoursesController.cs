using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using TeacherControl.API.Extensors;
using TeacherControl.Common.Enums;
using TeacherControl.Common.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Queries;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Controllers
{
    [Authorize]
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
                    ["filters"] = filtersDto.ToJson(),
                    ["results"] = data.ToJsonArray(),
                };

                return json;
            });
        }

        [HttpGet, Route("{courseId:int:min(1)}")]
        public IActionResult GetCourseById([FromRoute] int courseId)
        {
            return this.Ok(() => _CourseRepo.GetById(courseId).ToJson());
        }

        [HttpPost]
        public IActionResult AddCourse([FromBody] CourseDTO dto)
        {
            return this.Created(() =>
            {
                bool result = _CourseRepo.Add(dto).Equals((int)TransactionStatus.SUCCESS);

                return result ? dto.ToJson() : new JObject();
            });
        }

        [HttpDelete, Route("{courseId:int:min(1)}")]
        public IActionResult DeleteCourse([FromRoute] int courseId)
        {
            int successTransactionValue = (int)TransactionStatus.SUCCESS;
            return this.NoContent(() => _CourseRepo.Remove(courseId).Equals(successTransactionValue));
        }

        [HttpPut, Route("{courseId:int:min(1)}")]
        public IActionResult UpdateCourse([FromRoute] int courseId, [FromBody] CourseDTO dto)
        {
            int successTransactionValue = (int)TransactionStatus.SUCCESS;

            return this.NoContent(() => _CourseRepo.Update(courseId, dto).Equals(successTransactionValue));
        }

        [HttpPatch, Route("{courseId:int:min(1)}/assign-credits/{userId:int:min(1)}")]
        public IActionResult AssignUserCredits([FromRoute] int courseId, [FromRoute] int userId, [FromBody] double credits)
        {
            int successTransactionValue = (int)TransactionStatus.SUCCESS;

            return this.NoContent(() => _CourseRepo.AssignUserCredits(courseId, userId, credits).Equals(successTransactionValue));
        }

        [HttpPost, Route("{courseId:int:min(1)}/subscribe/{userId:int:min(1)}")]
        public IActionResult SubscribeStudent([FromRoute] int courseId, [FromRoute] int userId)
        {
            int successTransactionValue = (int)TransactionStatus.SUCCESS;

            return this.NoContent(() => _CourseRepo.SubscribeUser(courseId, userId).Equals(successTransactionValue));
        }

        [HttpPost, Route("{courseId:int:min(1)}/subscribe")]
        public IActionResult SubscribeStudents([FromRoute] int courseId, [FromBody] IEnumerable<int> Users)
        {
            int successTransactionValue = (int)TransactionStatus.SUCCESS;

            return this.NoContent(() => _CourseRepo.SubscribeUsers(courseId, Users).Equals(successTransactionValue));
        }

    }
}
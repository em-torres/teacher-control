using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TeacherControl.API.Extensors;
using TeacherControl.Common.Enums;
using TeacherControl.Common.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Queries;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    public class UserController : Controller
    {
        protected readonly IUserRepository _UserRepo;

        public UserController(IUserRepository UserRepo)
        {
            _UserRepo = UserRepo;
        }

        [HttpGet]
        public IActionResult GetAllUsers([FromQuery] UserQuery query)
        {
            return this.Ok(() =>
            {
                JObject json = new JObject
                {
                    ["filters"] = query.ToJson(),
                    ["data"] = _UserRepo.GetAll(query).ToJsonArray()
                };

                return json;
            });
        }

        [HttpPost, Route("{UserId:int:min(1)}/create-userinfo")]
        public IActionResult CreateUserInfo([FromRoute] int UserId, [FromBody] UserInfoDTO dto)
        {
            //return this.Created(() => _UserRepo.Add(username, dto));
            return null;
        }

        [HttpPost, Route("is-username-available")]
        //TODO: Implement regex for the username here
        public IActionResult IsUsernameAvailable([FromBody] string Username)
        {
            return Json(_UserRepo.IsUsernameAvailable(Username));
        }

        [HttpPost, Route("is-email-available")]
        //TODO: Implement regex for the username here
        public IActionResult IsEmailAvailable([FromBody] string Email)
        {
            return Json(_UserRepo.IsEmailAvailable(Email));
        }

        [HttpDelete, Route("{UserId:int:min(1)}/remove-user")]
        public IActionResult RemoveUser([FromRoute] int UserId)
        {
            return this.NoContent(() => _UserRepo.Remove(UserId).Equals((int)TransactionStatus.SUCCESS));
        }

        [HttpPost, Route("{UserId:int:min(1)}/create-userinfo")]
        public IActionResult CreatedUserInfo([FromRoute] int UserId, [FromBody] UserInfoDTO dto)
        {
            return this.Created(() =>
            {
                if(_UserRepo.AddUserInfo(UserId, dto).Equals((int)TransactionStatus.SUCCESS)) return dto.ToJson();
                return new JObject();
            });
        }

        [HttpPut, Route("{UserId:int:min(1)}/update-userinfo")]
        public IActionResult UpdateUserInfo([FromRoute] int UserId, [FromBody] UserInfoDTO dto)
        {
            return this.NoContent(() =>_UserRepo.AddUserInfo(UserId, dto).Equals((int)TransactionStatus.SUCCESS));
        }

        [HttpGet, Route("{UserId:int:min(1)}/course-credits")]
        public IActionResult GetUserCourseCredits([FromRoute] int UserId)
        {
            return this.Ok(() => _UserRepo.GetCourseCredits(UserId).ToJson());
        }

        [HttpGet, Route("{UserId:int:min(1)}/course-credits/{CourseId:int:min(1)}")]
        public IActionResult GetUserCourseCreditsByCourseId([FromRoute] int UserId, [FromRoute] int CourseId)
        {
            return this.Ok(() => _UserRepo.GetCourseCreditsByCourseId(UserId, CourseId).ToJson());
        }
    }
}
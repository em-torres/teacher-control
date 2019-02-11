using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TeacherControl.Common;
using TeacherControl.Common.Extensors;
using TeacherControl.Core.Interfaces.Repositories;
using TeacherControl.Core.Models;
using TeacherControl.Infrastructure;
using TeacherControl.Infrastructure.Repositories;
using TeacherControl.WebApi.Models;

namespace TeacherControl.WebApi.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        public readonly TCContext _TCContext;
        public IUserRepository _UserRepository;
        public IMapper _Mapper;

        public UsersController(IMapper mapper)
        {
            _Mapper = mapper;
            _TCContext = new TCContext();
            _UserRepository = new UserRepository(_TCContext);
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (UnitOfWork unit = new UnitOfWork(_TCContext))
            {
                UserRepository repo = new UserRepository(_TCContext);
                var result = repo.GetAll();

                return Ok(result.ToList());
            }
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserViewModel viewModel)
        {
            return this.Created(() =>
            {
                _UserRepository.Add(_Mapper.Map<UserViewModel, User>(viewModel));

                using (UnitOfWork unit = new UnitOfWork(_TCContext))
                {
                    if (unit.Commit() > 0)
                    {
                        return JObject.FromObject(viewModel);
                    };
                }

                return new JObject();
            });
        }

        [HttpPost]
        public IActionResult UpdateUser([FromQuery(Name = "id")] int userID, [FromBody] UserViewModel viewModel)
        {
            if (userID > 0)
            {
                var props = new
                {
                    viewModel.Password,
                    viewModel.Username,
                    //UpdatedBy = this.User.Identity.Name,
                    UpdatedBy = "Test",
                    viewModel.Email,
                };
                _UserRepository.Update(i => i.Id.Equals(userID), props);
            }

            return this.Ok(() =>
            {
                using (UnitOfWork unit = new UnitOfWork(_TCContext))
                {
                    if (unit.Commit() > 0)
                    {
                        return JObject.FromObject(viewModel);
                    };
                }

                return new JObject();
            });
        }

        [HttpDelete]
        public IActionResult DeleteUser([FromQuery(Name = "id")] int userID)
        {
            if (userID > 0)
            {
                var props = new
                {
                    Status = CommonConstants.Status.InActive,
                    //UpdatedBy = this.User.Identity.Name,
                    UpdatedBy = "Test",
                };
                _UserRepository.Update(i => i.Id.Equals(userID), props);
            }

            return this.NoContent(() =>
            {
                using (UnitOfWork unit = new UnitOfWork(_TCContext))
                {
                    return unit.Commit() > 0;
                }

                return false;
            });
        }

        [HttpPatch]
        [Route("api/users/{id:int}/userInfo")]
        public IActionResult UpdateUserInfo([FromQuery(Name = "id")] int userID, UserViewModel ViewModel)
        {
            if (userID > 0)
            {
                var props = new
                {
                    UserInfo = ViewModel.UserInfo,
                    //UpdatedBy = this.User.Identity.Name,
                    UpdatedBy = "Test",
                };
                _UserRepository.Update(i => i.Id.Equals(userID), props);
            }

            return this.Ok(() =>
            {
                using (UnitOfWork unit = new UnitOfWork(_TCContext))
                {
                    if (unit.Commit() > 0)
                    {
                        return JObject.FromObject(ViewModel.UserInfo);
                    };
                }

                return new JObject();
            });
        }

    }
}
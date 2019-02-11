using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeacherControl.Core.Interfaces.Repositories;
using TeacherControl.Core.Models;
using TeacherControl.Infrastructure;
using TeacherControl.Infrastructure.Repositories;
using TeacherControl.WebApi.Models;
using TeacherControl.Common.Extensors;
using TeacherControl.Common;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace TeacherControl.WebApi.Controllers
{
    [Route("api/groups")]
    public class GroupsController : Controller
    {
        protected TCContext _TCContext;
        protected IGroupRepository _GroupRepository;
        protected IStatusRepository _StatusRepository;

        public GroupsController()
        {
            _TCContext = new TCContext();
            _GroupRepository = new GroupRepository(_TCContext);
            _StatusRepository = new StatusRepository(_TCContext);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.Ok(() =>
            {
                IQueryable<Core.Models.Group> groups = _GroupRepository.GetAll();



                return groups;
            });
        }

        #region Query params Filtering
        public IQueryable<Core.Models.Group> GetByName(IQueryable<Core.Models.Group> groups, string name)
        {
            Regex regex = new Regex(@"[a-zA-Z0-9\_]+");
            if (name.Length > 0 && regex.Matches(name).Count == 1)
            {
                groups = groups.Where(i => i.Name.Equals(name));
            }
            return groups;
        }

        public IQueryable<Core.Models.Group> GetByPrivileges(IQueryable<Core.Models.Group> groups, string privilegeNames)
        {
            Regex regex = new Regex(@"[a-zA-Z0-9\_]+");
            IEnumerable<string> names = privilegeNames.Split(',');

            if (regex.Matches(privilegeNames).Count.Equals(names.Count()))
            {
                groups = groups
                    .Where(i => i.Privileges.Any(
                        m => names.Contains(m.Name)));
            }
            return groups;
        }

        public IQueryable<Core.Models.Group> GetByUserName(IQueryable<Core.Models.Group> groups, string name)
        {
            //if (Regex.IsMatch(name, @"[a-zA-Z0-9\_]+"))
            //{
            //    groups = groups
            //        .Where(i => i.Users.Any(
            //            m => names.Contains(m.Name)));
            //}
            return groups;
        }

        public IQueryable<Core.Models.Group> GetAssignmentsByGroupName(IQueryable<Core.Models.Group> groups, string name)
        {
            //if (Regex.IsMatch(name, @"[a-zA-Z0-9\_]+"))
            //{
            //    groups = groups
            //        .Where(i => i.ass.Any(
            //            m => names.Contains(m.Name)));
            //}
            return groups;
        }

        #endregion

        [HttpPost]
        public IActionResult SaveGroup([FromBody] GroupViewModel groupModelView)
        {
            return this.Ok(() =>
            {
                using (UnitOfWork unit = new UnitOfWork(_TCContext))
                {
                    Core.Models.Group group = BuildGroup(groupModelView);
                    _GroupRepository.Add(group);
                    unit.Commit();
                    return JObject.FromObject(group);
                }
            });
        }

        [HttpDelete]
        public IActionResult DeleteGroup([FromQuery(Name = "id")] int Id)
        {
            return this.NoContent(() =>
            {
                lock (new object())
                {
                    Core.Models.Group group = _GroupRepository.Find(i => i.Id.Equals(Id));
                    group.Status = _StatusRepository.GetById((int)CommonConstants.Status.InActive);

                    using (UnitOfWork unit = new UnitOfWork(_TCContext))
                    {
                        _GroupRepository.Update(i => i.Id.Equals(Id), group);
                        return unit.Commit() > 0;
                    }
                }
            });
        }

        [HttpPut]
        public IActionResult UpdateGroup([FromQuery(Name = "Id")] int Id, [FromBody] GroupViewModel groupModelView)
        {
            lock (new object())
            {
                return this.NoContent(() =>
                {
                    if (Id > 0)
                    {
                        Core.Models.Group group = BuildGroup(groupModelView);
                        Core.Models.Group oldgroup = _GroupRepository.Find(i => i.Id.Equals(Id));

                        using (UnitOfWork unit = new UnitOfWork(_TCContext))
                        {
                            _GroupRepository.Update(i => i.Id.Equals(Id), group);
                            return unit.Commit() > 0;
                        }
                    }

                    return false;
                });
            }
        }

        [HttpGet]
        public IActionResult IsAvailable([FromQuery(Name = "name")] string name)
        {
            try
            {
                return Json(_GroupRepository.GetAll(i => i.Name.Equals(name)).Any() == false);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex); //TODO: improve this
            }
            catch (Exception ex)
            {
                return BadRequest(ex); //TODO: improve this
            }
        }

        private Core.Models.Group BuildGroup(GroupViewModel groupModelView)
        {
            Core.Models.Group group = new Core.Models.Group
            {
                CreatedBy = User.Identity.Name,
                UpdatedBy = User.Identity.Name,
                Name = groupModelView.Name,
                Status = _StatusRepository.GetById(groupModelView.Status)
            };

            return group;
        }
    }
}
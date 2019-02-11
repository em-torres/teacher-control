using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.RegularExpressions;
using TeacherControl.API.Extensors;
using TeacherControl.Domain.QueryFilters;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Controllers
{
    [Route("api/assignments")]
    public class AssignmentsController : Controller
    {
        protected IAssignmentRepository _AssignmentRepo;
        protected IGroupRepository _GroupRepo;
        protected IStatusRepository _StatusRepo;
        protected IMapper _Mapper;

        public AssignmentsController(
            IMapper mapper, IAssignmentRepository assignmentRepo,
            IGroupRepository groupRepo, IStatusRepository statusRepo)
        {
            _AssignmentRepo = assignmentRepo;
            _GroupRepo = groupRepo;
            _StatusRepo = statusRepo;
            _Mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] AssignmentQueryFilter filters)
        {
            string pattern = @"([\w\-#@]+(?=,)*)";//allows [a-z0-9_-@#]

            MatchCollection groupMatches = new Regex(pattern).Matches(filters.Groups ?? string.Empty);
            MatchCollection typeMatches = new Regex(pattern).Matches(filters.Types ?? string.Empty);
            MatchCollection tagsMatches = new Regex(pattern).Matches(filters.Tags ?? string.Empty);

            filters.Groups = string.Join(",", groupMatches.Select(i => i.Value.ToString()));
            filters.Types = string.Join(",", typeMatches.Select(i => i.Value.ToString()));
            filters.Tags = string.Join(",", tagsMatches.Select(i => i.Value.ToString()));

            return this.Ok(() => _AssignmentRepo.GetAllByFilters(filters).Result);
        }

        //[HttpPost]
        //public IActionResult AddAssignment([FromBody] AssignmentViewModel modelView)
        //{
        //    return this.Created(() =>
        //    {
        //        JObject result = new JObject();

        //        using (UnitOfWork unit = new UnitOfWork(_TCContext))
        //        {
        //            Assignment model = _Mapper.Map<AssignmentViewModel, Assignment>(modelView);

        //            model.Tags = modelView.Tags.Select(i => new AssignmentTag { Name = i }).ToList();
        //            model.Types = modelView.Types.Select(i => new AssignmentType { Name = i }).ToList();
        //            model.Groups = _GroupRepo.GetAllByNames(modelView.Groups).Select(i => new AssignmentGroup { Group = i, Assignment = model }).ToList();

        //            model.Status = _StatusRepo.GetById((int)CommonConstants.Status.Active);
        //            //this.SetModelUserAudit(model);
        //            model.CreatedBy = "Test";
        //            model.UpdatedBy = "Test";
        //            //TODO: remove this, put the UserIdentity name

        //            _AssignmentRepo.Add(model);

        //            if (unit.Commit() > 0)
        //            {
        //                modelView.Status = model.Status.Id;
        //                result = JObject.FromObject(modelView);
        //            }

        //            return result;
        //        }
        //    });
        //}

        //[HttpDelete]
        //public IActionResult DeleteAssignment([FromQuery(Name = "id")] string assignmentID)
        //{
        //    if (!string.IsNullOrWhiteSpace(assignmentID))
        //    {
        //        long id = 0;
        //        if (Regex.IsMatch(assignmentID, @"\d+") && long.TryParse(assignmentID, out id))
        //        {
        //            return UpdateAssignmentById(id);
        //        }
        //        else if (Regex.IsMatch(assignmentID.ToLower(), @"([a-f0-9]{12})"))
        //        {
        //            return UpdateAssignmentByTokenId(assignmentID);
        //        }
        //        else
        //        {
        //            return BadRequest("Invalid ID");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Invalid ID");
        //    }
        //}

        //private IActionResult UpdateAssignmentByTokenId(string assignmentID)
        //{
        //    return this.NoContent(() =>
        //    {
        //        bool isUpdated = false;
        //        using (UnitOfWork unit = new UnitOfWork(_TCContext))
        //        {
        //            int statusID = (int)CommonConstants.Status.InActive;
        //            Status status = _StatusRepo.GetById(statusID);
        //            Assignment assignment = _AssignmentRepo.Find(i => i.HashIndex.Equals(assignmentID));

        //            if (assignment.Status.Id.Equals(statusID))
        //            {
        //                assignment.Status = status;

        //                _AssignmentRepo.Update(i => i.Id.Equals(assignment.Id), assignment);

        //                isUpdated = unit.Commit() > 0;
        //            }
        //        }

        //        return isUpdated;
        //    });
        //}

        //protected IActionResult UpdateAssignmentById(long id)
        //{
        //    return this.NoContent(() =>
        //    {
        //        using (UnitOfWork unit = new UnitOfWork(_TCContext))
        //        {
        //            int statusID = (int)CommonConstants.Status.InActive;
        //            Status status = _StatusRepo.GetById(statusID);
        //            Assignment assignment = _AssignmentRepo.Find(i => i.Id.Equals(id));
        //            assignment.Status = status;

        //            _AssignmentRepo.Update(i => i.Id.Equals(id), assignment);

        //            return unit.Commit() > 0;
        //        }
        //    });
        //}

        //[HttpPut]
        //public IActionResult UpdateAssignment([FromQuery(Name = "id")] int Id, [FromBody] AssignmentViewModel viewModel)
        //{
        //    return this.Ok(() =>
        //    {
        //        if (Id > 0)
        //        {
        //            lock (new object())
        //            {
        //                using (UnitOfWork unit = new UnitOfWork(_TCContext))
        //                {
        //                    Assignment old = _AssignmentRepo.Find(i => i.Id.Equals(Id));
        //                    Assignment newModel = _Mapper.Map<AssignmentViewModel, Assignment>(viewModel);
        //                    Status status = _StatusRepo.GetById(viewModel.Status);

        //                    _AssignmentRepo.DeleteDependecies(old);
        //                    //this.SetModelUserAudit(newModel);

        //                    newModel.Tags = viewModel.Tags.Select(i => new AssignmentTag { Name = i }).ToList();
        //                    newModel.Types = viewModel.Types.Select(i => new AssignmentType { Name = i }).ToList();
        //                    newModel.Groups = _GroupRepo.GetAllByNames(viewModel.Groups).Select(i => new AssignmentGroup { Group = i, Assignment = old });
        //                    newModel.UpdatedDate = DateTime.UtcNow;

        //                    //TODO: remove this
        //                    newModel.UpdatedBy = "Test2";
        //                    newModel.UpdatedDate = DateTime.UtcNow;

        //                    _AssignmentRepo.Update(i => i.Id.Equals(Id), newModel);

        //                    if (unit.Commit() > 0)
        //                    {
        //                        return JObject.FromObject(viewModel);
        //                    }
        //                }

        //            }
        //        }

        //        return new JObject();
        //    });
        //}

        //[HttpPatch, Route("update-count")]
        //public IActionResult UpdateAssignmentCounts([FromBody] JObject json)
        //{
        //    try
        //    {
        //        int id = int.Parse(json["id"].ToString());
        //        bool upvotesCount = bool.Parse(json["viewCount"].ToString());
        //        bool viewsCount = bool.Parse(json["upvoteCount"].ToString());

        //        Assignment assignment = new Assignment();
        //        lock (new object())
        //        {
        //            using (UnitOfWork unit = new UnitOfWork(_TCContext))
        //            {
        //                assignment = _AssignmentRepo.Find(i => i.Id.Equals(id));
        //                assignment.UpvotesCount += upvotesCount ? 1 : 0;
        //                assignment.ViewsCount += viewsCount ? 1 : 0;

        //                if (unit.Commit() > 0)
        //                {
        //                    return Accepted(new { upvotesCount = assignment.UpvotesCount, viewsCount = assignment.ViewsCount });
        //                }
        //            }
        //        }
        //        return BadRequest("Invalid json");
        //    }
        //    catch (SqlException ex)
        //    {
        //        JsonResult result = new JsonResult(ex)
        //        {
        //            StatusCode = (int)HttpStatusCode.InternalServerError,
        //            Value = new { message = ex.Message, details = ex.InnerException != null ? ex.InnerException.Message : "" }
        //        };//TODO: get a more valuable sqlserver error
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        JsonResult result = new JsonResult(ex)
        //        {
        //            StatusCode = (int)HttpStatusCode.InternalServerError,
        //            Value = new { message = ex.Message, details = ex.InnerException != null ? ex.InnerException.Message : "" }
        //        };//TODO: get a more valuable sqlserver error
        //        return result;
        //    }

        //    return BadRequest();
        //}

        //[HttpGet, Route("is-available")]
        //public IActionResult IsAvailable([FromQuery(Name = "title")] string titleName)
        //{
        //    Regex regex = new Regex(@"[a-z0-9\ ]+");
        //    if (titleName.Length > 0 && regex.Matches(titleName).Count == 1)
        //    {
        //        try
        //        {
        //            return Json(_AssignmentRepo.GetAll(i => i.Title.Equals(titleName)).Any() == false);
        //        }
        //        catch (SqlException ex)
        //        {
        //            return BadRequest(ex); //TODO: improve this
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex); //TODO: improve this
        //        }
        //    }
        //    return BadRequest("Parameters are not valid");//TODO: improve err message
        //}
    }
}
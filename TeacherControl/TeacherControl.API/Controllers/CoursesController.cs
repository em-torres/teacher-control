using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeacherControl.Core.Interfaces.Repositories;
using TeacherControl.Core.Models;
using TeacherControl.Infrastructure;
using TeacherControl.Common.Extensors;
using TeacherControl.WebApi.Models;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using TeacherControl.Infrastructure.Repositories;
using TeacherControl.Common;
using System.Data.SqlClient;

namespace TeacherControl.WebApi.Controllers
{
    public class CoursesController : Controller
    {
        protected TCContext _TCContext { get; set; }
        protected ICourseRepository _CourseRepository { get; set; }
        protected IMapper _Mapper;

        public CoursesController(IMapper mapper)
        {
            _Mapper = mapper;
            _CourseRepository = new CourseRepository(_TCContext);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return this.Ok(() =>
            {
                IQueryable<Course> courses = _CourseRepository.GetAll();

                courses = GetByCredits(courses);
                courses = GetByProfessor(courses);
                courses = GetByDates(courses);
                courses = GetByTags(courses);
                courses = GetByTitle(courses);

                return courses;
            });
        }

        #region Query Params Filters
        public IQueryable<Course> GetByTitle(IQueryable<Course> courses)
        {
            string title = Request.Query.Where(i => i.Key.Equals("name")).FirstOrDefault().Value.ToString();

            if (string.IsNullOrEmpty(title) == false)
            {
                MatchCollection titleRegex = new Regex(@"([a-z0-9]+(?=\-))").Matches(title);
                Match indexRegex = new Regex(@"((?<=\-)*[a-f0-9]{12})$").Match(title);

                if (indexRegex.Value.Length == 12)
                {
                    string aTitle = string.Join(' ', titleRegex.Select(m => m.Value).SkipLast(1));
                    return courses
                        .Where(i =>
                            i.Name.ToLower().Contains(indexRegex.Value.ToLower()) ||
                            i.Name.ToLower().Contains(string.Join(' ', aTitle.ToLower())));
                }
            }
            return courses;
        }

        public IQueryable<Course> GetByDates(IQueryable<Course> courses)
        {
            //DATE FORMAT YYYY-MM-DD
            string startDate = Request.Query.Where(i => i.Key.ToLower().Equals("start_date")).FirstOrDefault().Value.ToString();
            string endDate = Request.Query.Where(i => i.Key.ToLower().Equals("end_date")).FirstOrDefault().Value.ToString();
            string datePattern = @"\d{4}\-\d{2}\-\d{2}";

            if (endDate.Length == 0 && Regex.IsMatch(startDate, datePattern))
            {
                if (DateTime.TryParse(startDate, out DateTime temp))
                {
                    return courses
                            .Where(i => i.StartDate.CompareTo(temp) >= 0);
                }
            }
            else if (startDate.Length == 0 && Regex.IsMatch(endDate, datePattern))
            {
                if (DateTime.TryParse(endDate, out DateTime temp))
                {
                    return courses.Where(i => i.EndDate.CompareTo(temp) >= 0);
                }
            }
            else
            {
                if (Regex.IsMatch(startDate, datePattern) && Regex.IsMatch(endDate, datePattern))
                {
                    DateTime start, end;
                    if (DateTime.TryParse(startDate, out start) && DateTime.TryParse(endDate, out end))
                    {
                        return courses.Where(i =>
                                    i.StartDate.CompareTo(start) >= 0 &&
                                    i.EndDate.CompareTo(end) <= 0);
                    }
                }
            }

            return courses;
        }

        public IQueryable<Course> GetByProfessor(IQueryable<Course> courses)
        {
            string professorName = Request.Query.Where(i => i.Equals("profesor")).FirstOrDefault().Value.ToString();

            if(string.IsNullOrEmpty(professorName) == false)
            {
                courses = courses.Where(i => 
                    i.Professor.UserInfo.FirstName.Contains(professorName) &&
                    i.Professor.UserInfo.LastName.Contains(professorName));
            }

            return courses;
        }

        public IQueryable<Course> GetByCredits(IQueryable<Course> courses)
        {
            string credits = Request.Query.Where(i => i.Key.Equals("credits")).FirstOrDefault().Value.ToString();

            if(string.IsNullOrEmpty(credits) == false && double.TryParse(credits, out double tmp) && tmp > 0.0d)
            {
                courses.Where(i =>
                    i.Credits >= tmp &&
                    i.Credits <= tmp);
            }

            return courses;
        }

        public IQueryable<Course> GetByTags(IQueryable<Course> courses)
        {
            string[] tagsList = Request.Query.Where(i => i.Key.Equals("tags")).FirstOrDefault().Value.ToString().Split(",");

            if(tagsList.Length > 0 && tagsList.All(i => string.IsNullOrEmpty(i) == false))
            {
                courses.Where(i => 
                    i.Tags.Select(m => m.Name).OrderBy(m => m)
                    .SequenceEqual(tagsList.AsEnumerable().OrderBy(m => m)));
            }

            return courses;
        }

        #endregion

        [HttpPost]
        public IActionResult AddCourse([FromBody] CourseViewModel viewModel)
        {
            return this.Created(() =>
            {
                Course course = _Mapper.Map<CourseViewModel, Course>(viewModel);

                course.Tags = (ICollection<CourseTag>) viewModel.Tags.Select(i => new CourseTag { Name = i });
                course.Types = (ICollection<CourseType>) viewModel.Tags.Select(i => new CourseType { Name = i });
                //this.SetModelUserAudit(course.Professor);

                //TODO: remove this
                course.Professor.CreatedBy = "Test";
                course.Professor.UpdatedBy = "Test";

                using (UnitOfWork unit = new UnitOfWork(_TCContext))
                {
                    _CourseRepository.Add(course);

                    if (unit.Commit() > 0)
                    {
                        return JObject.FromObject(viewModel);
                    }
                }

                return new JObject();
            });
        }

        [HttpDelete]
        public IActionResult DeleteCourse([FromQuery(Name = "id")] int courseId)
        {
            return this.NoContent(() =>
            {
                JObject json = new JObject();

                if(courseId > 0)
                {
                    using (UnitOfWork unit = new UnitOfWork(_TCContext))
                    {
                        Course course = _CourseRepository.Find(i => i.Id.Equals(courseId));
                        _CourseRepository.Update(i => i.Id.Equals(courseId), new { Status = CommonConstants.Status.InActive });

                        return unit.Commit() > 0;
                    }
                }

                return false;
            });
        }


        [HttpPut]
        public IActionResult UpdateCourse([FromQuery(Name = "id")] int courseId, CourseViewModel viewModel)
        {
            return this.Ok(() =>
            {
                JObject json = new JObject();
                lock (new object())
                {
                    Course newCourse = _Mapper.Map<CourseViewModel, Course>(viewModel);

                    newCourse.Tags = (ICollection<CourseTag>)viewModel.Tags.Select(i => new CourseTag { Name = i });
                    newCourse.Types = (ICollection<CourseType>)viewModel.Tags.Select(i => new CourseType { Name = i });
                    //this.SetModelUserAudit(course.Professor);

                    //TODO: remove this
                    newCourse.Professor.UpdatedBy = "Test";

                    using (UnitOfWork unit = new UnitOfWork(_TCContext))
                    {
                        _CourseRepository.Update(i => i.Id.Equals(courseId), newCourse);

                        if (unit.Commit() > 0)
                        {
                            return JObject.FromObject(viewModel);
                        }
                    }
                }

                return json;
            });
        }


        [HttpGet, Route("is-available")]
        public IActionResult IsAvailable([FromQuery(Name = "name")] string titleName)
        {
            Regex regex = new Regex(@"[a-z0-9\ ]+");
            if (titleName.Length > 0 && regex.Matches(titleName).Count == 1)
            {
                try
                {
                    return Json(_CourseRepository.GetAll(i => i.Name.Equals(titleName)).Any() == false);
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
            return BadRequest("Parameters are not valid");//TODO: improve err message
        }
    }
}
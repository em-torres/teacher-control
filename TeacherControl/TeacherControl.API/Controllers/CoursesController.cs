using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TeacherControl.API.Extensors;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;
using TeacherControl.Domain.Queries;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Controllers
{
    public class CoursesController : Controller
    {
        protected readonly ICourseRepository _CourseRepo;

        public CoursesController(ICourseRepository courseRepository)
        {
            _CourseRepo = courseRepository;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] CourseQuery filtersDto)
        {
            //filters
            //return Ok(() => _CourseRepo.GetAll(filtersDto));
            return null;
        }

        //#region Query Params Filters
        //public IQueryable<Course> GetByTitle(IQueryable<Course> courses)
        //{
        //    string title = Request.Query.Where(i => i.Key.Equals("name")).FirstOrDefault().Value.ToString();

        //    if (string.IsNullOrEmpty(title) == false)
        //    {
        //        MatchCollection titleRegex = new Regex(@"([a-z0-9]+(?=\-))").Matches(title);
        //        Match indexRegex = new Regex(@"((?<=\-)*[a-f0-9]{12})$").Match(title);

        //        if (indexRegex.Value.Length == 12)
        //        {
        //            string aTitle = string.Join(' ', titleRegex.Select(m => m.Value).SkipLast(1));
        //            return courses
        //                .Where(i =>
        //                    i.Name.ToLower().Contains(indexRegex.Value.ToLower()) ||
        //                    i.Name.ToLower().Contains(string.Join(' ', aTitle.ToLower())));
        //        }
        //    }
        //    return courses;
        //}

        //public IQueryable<Course> GetByDates(IQueryable<Course> courses)
        //{
        //    //DATE FORMAT YYYY-MM-DD
        //    string startDate = Request.Query.Where(i => i.Key.ToLower().Equals("start_date")).FirstOrDefault().Value.ToString();
        //    string endDate = Request.Query.Where(i => i.Key.ToLower().Equals("end_date")).FirstOrDefault().Value.ToString();
        //    string datePattern = @"\d{4}\-\d{2}\-\d{2}";

        //    if (endDate.Length == 0 && Regex.IsMatch(startDate, datePattern))
        //    {
        //        if (DateTime.TryParse(startDate, out DateTime temp))
        //        {
        //            return courses
        //                    .Where(i => i.StartDate.CompareTo(temp) >= 0);
        //        }
        //    }
        //    else if (startDate.Length == 0 && Regex.IsMatch(endDate, datePattern))
        //    {
        //        if (DateTime.TryParse(endDate, out DateTime temp))
        //        {
        //            return courses.Where(i => i.EndDate.CompareTo(temp) >= 0);
        //        }
        //    }
        //    else
        //    {
        //        if (Regex.IsMatch(startDate, datePattern) && Regex.IsMatch(endDate, datePattern))
        //        {
        //            DateTime start, end;
        //            if (DateTime.TryParse(startDate, out start) && DateTime.TryParse(endDate, out end))
        //            {
        //                return courses.Where(i =>
        //                            i.StartDate.CompareTo(start) >= 0 &&
        //                            i.EndDate.CompareTo(end) <= 0);
        //            }
        //        }
        //    }

        //    return courses;
        //}

        //public IQueryable<Course> GetByProfessor(IQueryable<Course> courses)
        //{
        //    string professorName = Request.Query.Where(i => i.Equals("profesor")).FirstOrDefault().Value.ToString();

        //    if (string.IsNullOrEmpty(professorName) == false)
        //    {
        //        courses = courses.Where(i =>
        //            i.Professor.UserInfo.FirstName.Contains(professorName) &&
        //            i.Professor.UserInfo.LastName.Contains(professorName));
        //    }

        //    return courses;
        //}

        //public IQueryable<Course> GetByCredits(IQueryable<Course> courses)
        //{
        //    string credits = Request.Query.Where(i => i.Key.Equals("credits")).FirstOrDefault().Value.ToString();

        //    if (string.IsNullOrEmpty(credits) == false && double.TryParse(credits, out double tmp) && tmp > 0.0d)
        //    {
        //        courses.Where(i =>
        //            i.Credits >= tmp &&
        //            i.Credits <= tmp);
        //    }

        //    return courses;
        //}

        //public IQueryable<Course> GetByTags(IQueryable<Course> courses)
        //{
        //    string[] tagsList = Request.Query.Where(i => i.Key.Equals("tags")).FirstOrDefault().Value.ToString().Split(",");

        //    if (tagsList.Length > 0 && tagsList.All(i => string.IsNullOrEmpty(i) == false))
        //    {
        //        courses.Where(i =>
        //            i.Tags.Select(m => m.Name).OrderBy(m => m)
        //            .SequenceEqual(tagsList.AsEnumerable().OrderBy(m => m)));
        //    }

        //    return courses;
        //}

        //#endregion

        [HttpPost]
        public IActionResult AddCourse([FromBody] CourseDTO dto)
        {
            return this.Created(() =>
            {
                JObject json = new JObject();
                if (_CourseRepo.Add(dto, this.GetUsername()) > 0)
                {
                    json = JObject.FromObject(dto);
                }

                return json;
            });
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
    }
}
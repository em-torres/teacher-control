using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherControl.Core.Models;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class CourseLinqExtensors
    {
        public static IQueryable<Course> GetByName(this IQueryable<Course> courses, string courseName) =>
            !string.IsNullOrEmpty(courseName) ? courses.Where(i => i.Name.ToLower().Contains(courseName.ToLower())) : courses;

        public static IQueryable<Course> GetByDatesRange(this IQueryable<Course> courses, DateTime StartDate, DateTime EndDate)
        {
            if (EndDate.Equals(DateTime.MinValue) && StartDate.CompareTo(DateTime.MinValue) > 0)
                courses = courses.Where(a => DateTime.Compare(StartDate, a.StartDate) >= 0);
            else if (StartDate.Equals(DateTime.MinValue) && EndDate.CompareTo(DateTime.MinValue) > 0)
                courses = courses.Where(a => DateTime.Compare(EndDate, a.EndDate) <= 0);
            else if (DateTime.Compare(StartDate, EndDate) < 0)
                courses = courses.Where(a => DateTime.Compare(StartDate, a.StartDate) >= 0 && DateTime.Compare(EndDate, a.EndDate) <= 0);

            return courses;
        }

        public static IQueryable<Course> GetByCreditsRange(this IQueryable<Course> courses, double CreditsFrom, double CreditsEnd)
        {
            if (CreditsEnd <= 0 && CreditsFrom > 0) courses = courses.Where(i => i.Credits >= CreditsFrom);
            else if (CreditsFrom <= 0 && CreditsEnd > 0) courses = courses.Where(i => i.Credits <= CreditsEnd);
            else if (CreditsFrom > 0 && CreditsEnd > 0) courses = courses.Where(i => i.Credits >= CreditsFrom && i.Credits <= CreditsEnd);

            return courses;
        }

        public static IQueryable<Course> GetByTags(this IQueryable<Course> courses, IEnumerable<string> Tags)
        {
            if (Tags is null) return courses;

            return Tags.Any() ? courses.Where(i => i.Tags.Select(t => t.Name).SequenceEqual(Tags)) : courses;
        }
    }
}

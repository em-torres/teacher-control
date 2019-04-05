using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TeacherControl.Core.Models;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class AssignmentLinqExtensor
    {
        public static IQueryable<Assignment> GetByTitle(this IQueryable<Assignment> assignments, string Title)
        {
            //Finds the Assignment ONLy fot the last 12 Id Digits
            if (!string.IsNullOrEmpty(Title))
            {
                Match titleIndex = new Regex(@"[a-f0-9]{12}$").Match(Title);
                assignments = assignments.Where(a => a.HashIndex.Equals(titleIndex.Value));
            }

            return assignments;
        }

        public static IQueryable<Assignment> GetByDatesRange(this IQueryable<Assignment> assignments, DateTime StartDate, DateTime EndDate)
        {
            /* 
             * Datetime.Compare(t1, t2): https://docs.microsoft.com/en-us/dotnet/api/system.datetime.compare?view=netcore-2.1
             * if the filters dont have a end_date, search an entry from the start_date
             * if the filters dont have a end_date, search an entry from the end_date
             * else seach between the [start_end]-[end_date] range
             */
            if (EndDate.Equals(DateTime.MinValue) && StartDate.CompareTo(DateTime.MinValue) > 0)
                assignments = assignments.Where(a => DateTime.Compare(StartDate, a.StartDate) >= 0);
            else if (StartDate.Equals(DateTime.MinValue) && EndDate.CompareTo(DateTime.MinValue) > 0)
                assignments = assignments.Where(a => DateTime.Compare(EndDate, a.EndDate) <= 0);
            else if (DateTime.Compare(StartDate, EndDate) < 0)
                assignments = assignments.Where(a => DateTime.Compare(StartDate, a.StartDate) >= 0 && DateTime.Compare(EndDate, a.EndDate) <= 0);

            return assignments;
        }

        public static IQueryable<Assignment> GetByPointsRange(this IQueryable<Assignment> assignments, double PointsFrom, double PointsEnd)
        {
            if (PointsEnd <= 0 && PointsFrom > 0) assignments = assignments.Where(i => i.Points >= PointsFrom);
            else if (PointsFrom <= 0 && PointsEnd > 0) assignments = assignments.Where(i => i.Points <= PointsEnd);
            else if (PointsFrom > 0 && PointsEnd > 0)assignments = assignments.Where(i => i.Points >= PointsFrom && i.Points <= PointsEnd);

            return assignments;
        }

        public static IQueryable<Assignment> GetByTags(this IQueryable<Assignment> assignments, string Tags)
        {
            if (Tags != null)
            {
                IEnumerable<string> tagsList = Tags.Split(',').AsEnumerable();
                return tagsList.Any() ? assignments.Where(i => i.Tags.Select(t => t.Name).SequenceEqual(tagsList)) : assignments;
            }

            return assignments;
        }

        public static IQueryable<Assignment> GetByGroups(this IQueryable<Assignment> assignments, string Groups)
        {
            IEnumerable<string> groupList = Groups.Split(',').AsEnumerable();
            return groupList.Any() ? assignments.Where(i => i.Tags.Select(t => t.Name).SequenceEqual(groupList)) : assignments;
        }
    }
}

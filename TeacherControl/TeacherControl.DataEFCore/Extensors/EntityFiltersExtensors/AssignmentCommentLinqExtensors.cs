using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherControl.Core.Models;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class AssignmentCommentLinqExtensors
    {
        public static IQueryable<AssignmentComment> GetByDates (this IQueryable<AssignmentComment> comments, DateTime StartDate, DateTime EndDate)
        {
            /* 
             * Datetime.Compare(t1, t2): https://docs.microsoft.com/en-us/dotnet/api/system.datetime.compare?view=netcore-2.1
             * if the filters dont have a end_date, search an entry from the start_date
             * if the filters dont have a end_date, search an entry from the end_date
             * else seach between the [start_end]-[end_date] range
             */
            if (EndDate.Equals(DateTime.MinValue) && StartDate.CompareTo(DateTime.MinValue) > 0)
                comments = comments.Where(a => DateTime.Compare(StartDate, a.CreatedDate) >= 0);
            else if (StartDate.Equals(DateTime.MinValue) && EndDate.CompareTo(DateTime.MinValue) > 0)
                comments = comments.Where(a => DateTime.Compare(EndDate, a.CreatedDate) <= 0);
            else if (DateTime.Compare(StartDate, EndDate) < 0)
                comments = comments.Where(a => DateTime.Compare(StartDate, a.CreatedDate) >= 0 && DateTime.Compare(EndDate, a.CreatedDate) <= 0);

            return comments;
        }

        public static IQueryable<AssignmentComment> GetByAuthor(this IQueryable<AssignmentComment> comments, string Author)
        {
            comments.Where(i =>
                i.CreatedBy.ToLower().Equals(Author.ToLower()) ||
                i.CreatedBy.ToLower().Contains(Author.ToLower()));

            return comments;
        }
    }
}

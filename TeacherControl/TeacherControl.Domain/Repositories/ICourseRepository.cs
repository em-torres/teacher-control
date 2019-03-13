using System.Collections.Generic;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Core.Queries;

namespace TeacherControl.Domain.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        int Add(CourseDTO dto, string username);
        int Update(int Id, CourseDTO dto, string username);
        int Remove(int Id, string username);

        int AddComment(int CourseId, CourseCommentDTO dto, string CreatedBy);
        int UpdateComment(int CourseId, CourseCommentDTO dto, string CreatedBy);
        IEnumerable<CourseCommentDTO> GetAllCourseComments(int CourseId, CourseCommentQuery Query);
        int DisableCourseComment(int CourseId, int CommentId);
        int UpdateUpvoteCourseComment(int CourseId, int CommentId, int Upvote);

        IEnumerable<CourseDTO> GetAll(CourseQuery courseQueryDTO);
    }
}

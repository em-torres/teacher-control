using System.Collections.Generic;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;
using TeacherControl.Domain.Queries;

namespace TeacherControl.Domain.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        int Add(CourseDTO dto, string username);
        int Update(int Id, CourseDTO dto, string username);
        int Remove(int Id, CourseDTO dto, string username);

        int AddComment(int CourseId, CourseCommentDTO dto, string CreatedBy);
        int UpdateComment(int CourseId, CourseCommentDTO dto, string CreatedBy);
        int GetAllCourseComments(int CourseId, CourseCommentQuery Query);
        int DisableCourseComment(int CourseId, int CommentId);
        int UpdateUpvoteCourseComment(int CourseId, int CommentId, int Upvote);

        IEnumerable<CourseDTO> GetAll(CourseQuery courseQueryDTO);
    }
}

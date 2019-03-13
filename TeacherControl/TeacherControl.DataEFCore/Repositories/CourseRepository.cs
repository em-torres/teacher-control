using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TeacherControl.Common.Extensors;
using TeacherControl.DataEFCore.Extensors;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;
using TeacherControl.Domain.Queries;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.DataEFCore.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(TCContext tCContext, IMapper mapper) : base(tCContext, mapper)
        {

        }

        public int Add(CourseDTO dto, string createdBy)
        {
            Course course = _Mapper.Map<CourseDTO, Course>(dto);

            course.Tags = dto.Tags.Select(t => new CourseTag { Name = t }).ToList();
            course.CreatedBy = createdBy;
            course.Status = _Context.Statuses.Where(i => i.Id.Equals((int)Domain.Enums.Status.Active)).First();

            return Add(course);
        }

        public int Update(int id, CourseDTO dto, string updatedBy)
        {
            Course course = _Mapper.Map<CourseDTO, Course>(dto);

            IEnumerable<string> tags = dto.Tags.Select(t => t.ToLower());
            course.Tags = tags.Select(t => new CourseTag { Name = t }).ToList();
            course.UpdatedBy = updatedBy;

            Update(i => i.Id.Equals(id), course);

            return _Context.SaveChanges();
        }

        public int Remove(int id, CourseDTO dto, string updatedBy)
        {
            Course course = _Mapper.Map<CourseDTO, Course>(dto);

            IEnumerable<string> tags = dto.Tags.Select(t => t.ToLower());
            course.Tags = tags.Select(t => new CourseTag { Name = t }).ToList();
            course.UpdatedBy = updatedBy;
            course.Status = _Context.Statuses.Where(i => i.Id.Equals((int)Domain.Enums.Status.Active)).First();

            Update(i => i.Id.Equals(id), course);

            return _Context.SaveChanges();
        }

        public IEnumerable<CourseDTO> GetAll(CourseQuery dto)
        {
            IQueryable<Course> courses = GetAll()
                .GetByName(dto.Name)
                .GetByDatesRange(dto.StartDate, dto.EndDate)
                .GetByCreditsRange(dto.CreditsFrom, dto.CreditsEnd)
                .GetByTags(dto.Tags)
                .Pagination(dto.Page, dto.PageSize);

            return _Mapper.Map<IEnumerable<Course>, IEnumerable<CourseDTO>>(courses);
        }

        public int AddComment(int CourseId, CourseCommentDTO dto, string CreatedBy)
        {
            Course course = Find(i => i.Id.Equals(CourseId));
            CourseComment comment = _Mapper.Map<CourseCommentDTO, CourseComment>(dto);
            comment.CreatedBy = CreatedBy;

            course.Comments.Add(comment);

            return _Context.SaveChanges();
        }

        public int UpdateComment(int CourseId, CourseCommentDTO dto, string CreatedBy)
        {
            throw new System.NotImplementedException();
        }

        public int GetAllCourseComments(int CourseId, CourseCommentQuery Query)
        {
            throw new System.NotImplementedException();
        }

        public int DisableCourseComment(int CourseId, int CommentId)
        {
            CourseComment comment = Find(i => i.Id.Equals(CourseId)).Comments.Where(i => i.Id.Equals(CommentId)).First();
            comment.StatusId = (int) Domain.Enums.Status.Disabled;

            return _Context.SaveChanges();
        }

        public int UpdateUpvoteCourseComment(int CourseId, int CommentId, int upvote)
        {
            CourseComment comment = Find(i => i.Id.Equals(CourseId)).Comments.Where(i => i.Id.Equals(CommentId)).First();
            comment.Upvote += upvote;

            return _Context.SaveChanges();
        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TeacherControl.Common.Extensors;
using TeacherControl.DataEFCore.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Core.Queries;
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
            Status status = _Context.Statuses.Where(i => i.Id.Equals((int)Core.Enums.Status.Active)).First();

            course.CreatedBy = createdBy;
            course.Status = status;
            course.Professors = new List<UserCourse>()
            {
                new UserCourse { User = _Context.Users.Where(i => i.Id.Equals(dto.Professor)).First() }
            };

            return Add(course);
        }

        public int Update(int id, CourseDTO dto, string updatedBy)
        {
            Course newCourse = _Mapper.Map<CourseDTO, Course>(dto);
            //Course oldCourse = _Context.Courses.Where(i => i.Id.Equals(id)).FirstOrDefault();

            if (_Context.Courses.Any(i => i.Id.Equals(id)))
            {
                Update( i => i.Id.Equals(id) , dto);
                return _Context.SaveChanges();
            }

            return 0;
        }

        public int Remove(int id, string updatedBy)
        {
            int removedStatusID = (int)Core.Enums.Status.Deleted;
            Course course = _Context.Courses.Where(i => i.Id.Equals(id)).First();

            if (course.StatusId != removedStatusID)
            {
                course.UpdatedBy = updatedBy;
                course.Status = _Context.Statuses.Where(i => i.Id.Equals(removedStatusID)).First();

                return _Context.SaveChanges();
            }

            return 0;
        }

        public IEnumerable<CourseDTO> GetAll(CourseQuery dto)
        {
            IEnumerable<string> tags = dto.Tags != null && dto.Tags.Length > 0
                ? dto.Tags.Split(",")
                : new List<string>(0).AsEnumerable();

            IQueryable<Course> courses = GetAll()
                .GetByName(dto.Name)
                .GetByDatesRange(dto.StartDate, dto.EndDate)
                .GetByCreditsRange(dto.CreditsFrom, dto.CreditsEnd)
                .GetByTags(tags)
                .Pagination(dto.Page, dto.PageSize);

            return Mapper.Map<IEnumerable<Course>, IEnumerable<CourseDTO>>(courses.ToList());
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

        public IEnumerable<CourseCommentDTO> GetAllCourseComments(int CourseId, CourseCommentQuery Query)
        {
            Course course= Find(i => i.Id.Equals(CourseId));
            IQueryable<CourseComment> comments = course.Comments.AsQueryable();

            return _Mapper.Map<IEnumerable<CourseComment>, IEnumerable<CourseCommentDTO>>(comments);
        }

        public int DisableCourseComment(int CourseId, int CommentId)
        {
            CourseComment comment = Find(i => i.Id.Equals(CourseId)).Comments.Where(i => i.Id.Equals(CommentId)).First();
            comment.StatusId = (int)Core.Enums.Status.Disabled;

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

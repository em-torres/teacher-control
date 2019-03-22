using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TeacherControl.Common.Enums;
using TeacherControl.Common.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Core.Queries;
using TeacherControl.DataEFCore.Extensors;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.DataEFCore.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(TCContext tCContext, IMapper mapper) : base(tCContext, mapper) { }

        public int Add(CourseDTO dto)
        {
            Course course = _Mapper.Map<CourseDTO, Course>(dto);
            course.StatusId = (int)Core.Enums.Status.Active;
            course.Professors = new List<UserCourse>()
            {
                new UserCourse { User = _Context.Users.Where(i => i.Id.Equals(dto.Professor)).First() }
            };

            return Add(course);
        }

        public int Update(int id, CourseDTO dto)
        {
            Course newCourse = _Mapper.Map<CourseDTO, Course>(dto);

            if (_Context.Courses.Any(i => i.Id.Equals(id)))
            {
                Update(i => i.Id.Equals(id), dto);
                return _Context.SaveChanges();
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }

        public int Remove(int id)
        {
            int removedStatusID = (int)Core.Enums.Status.Deleted;
            Course course = _Context.Courses.Where(i => i.Id.Equals(id)).First();

            if (course.StatusId != removedStatusID)
            {
                course.Status = _Context.Statuses.Where(i => i.Id.Equals(removedStatusID)).First();
                return _Context.SaveChanges();
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
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

            return _Mapper.Map<IEnumerable<Course>, IEnumerable<CourseDTO>>(courses.ToList());
        }

        public CourseDTO GetById(int courseId)
        {
            Course course = Find(i => i.Id.Equals(courseId));
            return _Mapper.Map<Course, CourseDTO>(course);
        }
    }
}

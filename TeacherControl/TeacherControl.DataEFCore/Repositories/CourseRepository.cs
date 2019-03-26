using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            
            return Add(course);
        }
        
        //TODO subscribe a student or a teacher to the course, 

        public int Update(int id, CourseDTO dto)
        {
            Course newCourse = _Mapper.Map<CourseDTO, Course>(dto);

            if (_Context.Courses.Any(i => i.Id.Equals(id)))
            {
                return Update(i => i.Id.Equals(id), dto);
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }

        public int Remove(int id)
        {
            int removedStatusID = (int)Core.Enums.Status.Deleted;
            Course course = Find(i => i.Id.Equals(id));

            if (course.StatusId != removedStatusID)
            {
                return Remove(course);
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

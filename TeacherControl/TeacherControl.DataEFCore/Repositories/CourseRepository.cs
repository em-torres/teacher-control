using AutoMapper;
using System;
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
            string deleted = Core.Enums.Status.Deleted.ToString();
            Course course = Find(i => i.Id.Equals(id));

            if (course.Status != deleted)
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

        public CourseDTO GetById(int CourseId)
        {
            Course course = Find(i => i.Id.Equals(CourseId));

            if (course is null || course.Id <= 0) return new CourseDTO();

            return _Mapper.Map<Course, CourseDTO>(course);
        }

        public int SubscribeUser(int CourseId, int UserId)
        {
            Course course = Find(i => i.Id.Equals(CourseId));
            User student = _Context.Users.Where(i => i.Id.Equals(UserId)).FirstOrDefault();

            if (course is null || course.Id <= 0) return (int)TransactionStatus.ENTITY_NOT_FOUND;
            if (student is null || student.Id <= 0) return (int)TransactionStatus.ENTITY_NOT_FOUND;

            CourseUserCredit userCredits = new CourseUserCredit
            {
                Course = course,
                Student = student
            };

            course.StudentCredits.Add(userCredits);
            return _Context.SaveChanges();
        }

        public int SubscribeUsers(int CourseId, IEnumerable<int> Users)
        {
            Course course = Find(i => i.Id.Equals(CourseId));
            bool areAllStudentsExisting = _Context.Users.All(i => Users.Contains(i.Id));

            if (course is null || course.Id <= 0)
            {
                if (areAllStudentsExisting)
                {
                    Users.ToList().ForEach(i => course.StudentCredits.Add(new CourseUserCredit { Course = course, StudentId = i }));
                    return _Context.SaveChanges();
                }
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }

        public int AssignUserCredits(int CourseId, int UserId, double Credits)
        {
            Course course = Find(i => i.Id.Equals(CourseId));
            User student = _Context.Users.Where(i => i.Id.Equals(UserId)).FirstOrDefault();

            if (course is null || course.Id <= 0) return (int)TransactionStatus.ENTITY_NOT_FOUND;
            if (student is null || student.Id <= 0) return (int)TransactionStatus.ENTITY_NOT_FOUND;

            if (Credits >= 0)
            {
                _Context.CourseUserCredits.Add(new CourseUserCredit { Course = course, Student = student, Credits = Credits });
                return _Context.SaveChanges();
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }
    }
}

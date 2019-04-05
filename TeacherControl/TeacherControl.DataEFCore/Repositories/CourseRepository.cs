using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TeacherControl.Common.Enums;
using TeacherControl.Common.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Enums;
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
            course.Professor = _Context.Users
                .Where(i => i.Id.Equals(dto.Professor) && i.Status.Equals(Status.Active.ToString()))
                .FirstOrDefault();

            if (course.Professor is null || course.Professor.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

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
            ;
            Course course = Find(i => i.Id.Equals(id));

            if (course is null && course.Id <= 0)
            {
                return (int)TransactionStatus.SUCCESS;
            }

            if (course.Status != Status.Deleted.ToString())
            {
                return (int)TransactionStatus.UNCHANGED;
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
            if (_Context.CourseUserCredits.Any(i => i.StudentId.Equals(student.Id))) return (int)TransactionStatus.UNCHANGED;

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
            Users = Users.Where(i => i > 0).Distinct();
            Course course = Find(i => i.Id.Equals(CourseId));
            bool areAllStudentsExisting = Users.All(i => _Context.Users.Any(u => u.Id.Equals(i)));

            if (course is null || course.Id <= 0)
            {
                return (int)TransactionStatus.ENTITY_NOT_FOUND;
            }

            if (areAllStudentsExisting)
            {
                IEnumerable<int> subscribedStudents = _Context.CourseUserCredits
                    .Where(i => Users.Contains(i.StudentId))
                    .Select(i => i.StudentId)
                    .Distinct();

                Users.Except(subscribedStudents)
                    .ToList().ForEach(i =>
                    {
                        course.StudentCredits.Add(new CourseUserCredit { Course = course, StudentId = i });
                    });
                return _Context.SaveChanges();
            }


            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }

        public int AssignUserCredits(int CourseId, int UserId, double Credits)
        {
            Course course = Find(i => i.Id.Equals(CourseId));
            User student = _Context.Users.Where(i => i.Id.Equals(UserId)).FirstOrDefault();

            if (course is null || course.Id <= 0) return (int)TransactionStatus.ENTITY_NOT_FOUND;
            if (student is null || student.Id <= 0) return (int)TransactionStatus.ENTITY_NOT_FOUND;

            if (course.Status.Equals(Status.Active.ToString()) && student.Status.Equals(Status.Active.ToString()))
            {
                if (Credits >= 0)
                {
                    CourseUserCredit userCredits = _Context.CourseUserCredits
                        .Where(i => i.CourseId.Equals(course.Id))
                        .Where(i => i.StudentId.Equals(student.Id))
                        .FirstOrDefault();

                    if (userCredits is null || userCredits.Id <= 0)
                        return (int)TransactionStatus.ENTITY_NOT_FOUND;

                    userCredits.Credits = Credits;
                    return _Context.SaveChanges();
                }

                return (int)TransactionStatus.UNCHANGED;
            }

            return (int)TransactionStatus.ENTITY_NOT_FOUND;
        }
    }
}

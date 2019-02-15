using AutoMapper;
using System.Collections.Generic;
using System.Linq;
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

            IEnumerable<string> tags = dto.Tags.Select(t => t.ToLower());
            course.Tags = tags.Select(t => new CourseTag { Name = t }).ToList();
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
            IQueryable<Course> courses = GetAll();

            if (!string.IsNullOrEmpty(dto.Name)) courses = courses.Where(i => i.Name.ToLower().Contains(dto.Name.ToLower()));

            if (dto.ExactCredits == 0 && dto.StartFrom > 0 && dto.EndFrom > 0 && dto.StartFrom <= dto.EndFrom)
                courses = courses.Where(i => dto.StartFrom >= i.Credits && dto.EndFrom <= i.Credits);

            if (dto.ExactCredits > 0) courses = courses.Where(i => i.Credits.Equals(dto.ExactCredits));

            courses = courses.Skip(dto.Page * dto.PageSize).Take(dto.PageSize > 0 ? dto.PageSize : 50);
            return _Mapper.Map<IEnumerable<Course>, IEnumerable<CourseDTO>>(courses);
        }
    }
}

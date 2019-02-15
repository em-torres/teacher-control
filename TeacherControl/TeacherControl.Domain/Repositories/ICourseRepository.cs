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

        IEnumerable<CourseDTO> GetAll(CourseQuery courseQueryDTO);
    }
}

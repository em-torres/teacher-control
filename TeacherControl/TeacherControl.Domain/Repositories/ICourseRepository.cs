using System.Collections.Generic;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Core.Queries;

namespace TeacherControl.Domain.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        int Add(CourseDTO dto);
        int Update(int Id, CourseDTO dto);
        int Remove(int Id);
        CourseDTO GetById(int CourseId);

        IEnumerable<CourseDTO> GetAll(CourseQuery courseQueryDTO);
    }
}

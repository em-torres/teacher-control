using System.Collections.Generic;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Core.Queries;

namespace TeacherControl.Domain.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        int Add(CourseDTO dto);
        int Update(int id, CourseDTO dto);
        int Remove(int id);
        IEnumerable<CourseDTO> GetAll(CourseQuery dto);
        CourseDTO GetById(int CourseId);
        int SubscribeUser(int CourseId, int UserId);
        int SubscribeUsers(int CourseId, IEnumerable<int> Users);
        int AssignUserCredits(int CourseId, int UserId, double Credits);
    }
}

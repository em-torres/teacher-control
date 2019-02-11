using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.QueryFilters;

namespace TeacherControl.Domain.Repositories
{
    public interface IAssignmentRepository : IRepository<AssignmentDTO>
    {
        void DeleteTags(AssignmentDTO AssignmentDTO);
        void DeleteGroups(AssignmentDTO AssignmentDTO);
        void DeleteTypes(AssignmentDTO AssignmentDTO);
        void DeleteDependecies(AssignmentDTO AssignmentDTO);

        Task<IEnumerable<AssignmentDTO>> GetAllByFilters(AssignmentQueryFilter filters);

        IEnumerable<AssignmentDTO> GetByTitle(string title);
        IEnumerable<AssignmentDTO> GetFromStartDate(DateTime startDate);
        IEnumerable<AssignmentDTO> GetFromEndDate(DateTime endDate);
        IEnumerable<AssignmentDTO> GetFromStartPoints(float startPoints);
        IEnumerable<AssignmentDTO> GetFromEndPoints(float endPoints);
        IEnumerable<AssignmentDTO> GetByExactPoints(float points);
        IEnumerable<AssignmentDTO> GetByGroups(IEnumerable<string> groups);
        IEnumerable<AssignmentDTO> GetByTypes(IEnumerable<string> types);
        IEnumerable<AssignmentDTO> GetByTags(IEnumerable<string> tags);
    }
}

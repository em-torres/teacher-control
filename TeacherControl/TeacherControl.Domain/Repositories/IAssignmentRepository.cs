using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Models;
using TeacherControl.Core.Queries;

namespace TeacherControl.Domain.Repositories
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        int Add(AssignmentDTO dto, string createBy);
        int DeleteById(int ID, string updatedBy);
        int DeleteByTokenId(string tokenID, string updatedBy);
        int Update(int id, AssignmentDTO dto, string updatedBy);
        int UpdateTags(int id, IEnumerable<string> tags, string updateBy);
        int UpdateGroups(int id, IEnumerable<string> groups, string updateBy);
        int UpdateUpvoteCount(int id, int value);
        int UpdateViewsCount(int id, int value);

        Task<IEnumerable<AssignmentDTO>> GetByFilters(AssignmentQuery dto);
        Task<IEnumerable<AssignmentResultDTO>> GetQuestionnaireResults(int Id, string username);
    }
}

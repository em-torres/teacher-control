using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Domain.DTOs;

namespace TeacherControl.Domain.Repositories
{
    public interface IGroupRepository : IRepository<GroupDTO>
    {
        IEnumerable<GroupDTO> GetAllByName(string GroupName);

        IEnumerable<GroupDTO> GetAllByNames(IEnumerable<string> GroupNames);
    }
}

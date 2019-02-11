using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.DataEFCore.Repositories
{
    public class GroupRepository : BaseRepository<GroupDTO>, IGroupRepository
    {
        public GroupRepository(TCContext Context, IMapper mapper) : base(Context, mapper)
        {
            _Context = Context;
        }

        public IEnumerable<GroupDTO> GetAllByName(string GroupName)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GroupDTO> GetAllByNames(IEnumerable<string> GroupNames)
        {
            return null;
            //return new GroupRepository(_Context).GetAll().Where(i => GroupNames.Any(m => m.Equals(i.Name))).AsEnumerable();
        }
    }
}

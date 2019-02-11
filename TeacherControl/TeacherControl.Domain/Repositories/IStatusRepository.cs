using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Domain.DTOs;

namespace TeacherControl.Domain.Repositories
{
    public interface IStatusRepository : IRepository<StatusDTO>
    {
        StatusDTO GetByName(string Name);
        StatusDTO GetById(int ID);
    }
}

using AutoMapper;
using System;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.DataEFCore.Repositories
{
    public class StatusRepository : BaseRepository<StatusDTO>, IStatusRepository
    {
        public StatusRepository(TCContext Context, IMapper mapper) : base(Context, mapper)
        {
        }

       
        StatusDTO IStatusRepository.GetById(int ID)
        {
            throw new NotImplementedException();
        }

        StatusDTO IStatusRepository.GetByName(string Name)
        {
            throw new NotImplementedException();
        }
    }
}

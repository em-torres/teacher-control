using AutoMapper;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.DataEFCore.Repositories
{
    public class UserRepository : BaseRepository<UserDTO>, IUserRepository
    {
        public UserRepository(TCContext Context, IMapper mapper) : base(Context, mapper)
        {
            _Context = Context;
        }
    }
}

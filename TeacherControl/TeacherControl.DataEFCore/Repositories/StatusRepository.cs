using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TeacherControl.Domain.DTOs;
using TeacherControl.Domain.Models;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.DataEFCore.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        protected TCContext _TCContext;

        public StatusRepository(TCContext Context)
        {
            _TCContext = Context;
        }

        public void Add(StatusDTO T)
        {
            throw new NotImplementedException();
        }

        public StatusDTO Find(Expression<Func<StatusDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<StatusDTO> FindAsync(Expression<Func<StatusDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StatusDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<StatusDTO> GetAll(Expression<Func<StatusDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<StatusDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<StatusDTO>> GetAllAsync(Expression<Func<StatusDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Status GetById(int ID)
        {
            return _TCContext
                .Set<Status>()
                .Where(i => i.Id.Equals(ID))
                .FirstOrDefault();
        }

        public Status GetByName(string Name)
        {
            return _TCContext
                .Set<Status>()
                .Where(i => i.Name.Equals(Name))
                .FirstOrDefault();
        }

        public void Remove(StatusDTO T)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<StatusDTO> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<StatusDTO, bool>> predicate, object properties)
        {
            throw new NotImplementedException();
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

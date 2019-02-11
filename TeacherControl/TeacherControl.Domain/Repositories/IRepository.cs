using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeacherControl.Domain.Models;

namespace TeacherControl.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Remove(TEntity T);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Add(TEntity T);
        void Update(Expression<Func<TEntity, bool>> predicate, object properties);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
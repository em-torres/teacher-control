using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.DataEFCore.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected TCContext _Context;
        protected IMapper _Mapper;

        public BaseRepository(TCContext Context, IMapper Mapper)
        {
            _Context = Context;
            _Mapper = Mapper;
        }

        public void Dispose()
        {
            _Context.Dispose();  //TBD
        }

        #region Repo CRUD
        public int Add(TEntity T)
        {
            _Context.Set<TEntity>().Add(T);
            return _Context.SaveChanges();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _Context
                .Set<TEntity>()
                .Where(predicate)
                .FirstOrDefault();
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _Context
                .Set<TEntity>()
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _Context.Set<TEntity>().Where(predicate).AsQueryable();
            return query;
        }

        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = _Context.Set<TEntity>().AsQueryable();
            return query;
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            IQueryable<TEntity> query = _Context.Set<TEntity>().AsQueryable();
            return Task.Factory.StartNew(() => query);
        }

        public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _Context.Set<TEntity>().Where(predicate).AsQueryable();
            return Task.Factory.StartNew(() => query);
        }

        public int Remove(TEntity T)
        {
            _Context.Set<TEntity>().Remove(T);
            return _Context.SaveChanges();
        }

        public int RemoveRange(IEnumerable<TEntity> entities)
        {
            _Context.Set<TEntity>().RemoveRange(entities);
            return _Context.SaveChanges();
        }

        public int Update(Expression<Func<TEntity, bool>> predicate, object properties)
        {
            var oldEntity = _Context.Set<TEntity>().Where(predicate).First();
            if (properties != null) _Context.Entry(oldEntity).CurrentValues.SetValues(properties);

            return _Context.SaveChanges();
        }

        #endregion
    }
}

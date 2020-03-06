using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MakeIt.Repository.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly IDbSet<TEntity> _dbSet;

        // ctor
        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        #region Create Methods
        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }
        #endregion

        #region Retrieve Methods
        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TType> Get<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class
        {
            return _dbSet.Where(where).Select(select).AsEnumerable();
        }

        public IEnumerable<TType> Get<TType>(Expression<Func<TEntity, TType>> select) where TType : class
        {
            return _dbSet.Select(select).AsEnumerable();
        }

        public IQueryable<TEntity> GetQuryableAll()
        {
            return _dbSet.AsQueryable();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsEnumerable();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }
        #endregion

        #region Update Methods
        public void Edit(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Edit(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Edit(entity);
            }
        }
        #endregion

        #region Delete Methods
        public void Delete(int id)
        {
            TEntity ent = _dbSet.Find(id);
            _dbSet.Remove(ent);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        #endregion
    }
}

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
        protected readonly IDbSet<TEntity> _dbset;

        // ctor
        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbset = context.Set<TEntity>();
        }

        #region Create Methods
        public TEntity Add(TEntity entity)
        {
            return _dbset.Add(entity);
        }
        #endregion

        #region Retrieve Methods
        public TEntity Get(int id)
        {
            return _dbset.Find(id);
        }

        public IEnumerable<TType> Get<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class
        {
            return _dbset.Where(where).Select(select).AsEnumerable();
        }

        public IEnumerable<TType> Get<TType>(Expression<Func<TEntity, TType>> select) where TType : class
        {
            return _dbset.Select(select).AsEnumerable();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbset.AsEnumerable();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Where(predicate).AsEnumerable();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.SingleOrDefault(predicate);
        }
        #endregion

        #region Update Methods
        public void Edit(TEntity entity)
        {
            _dbset.Attach(entity);
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
            TEntity ent = _dbset.Find(id);
            _dbset.Remove(ent);
        }

        public void Delete(TEntity entity)
        {
            _dbset.Remove(entity);
        }
        #endregion
    }
}

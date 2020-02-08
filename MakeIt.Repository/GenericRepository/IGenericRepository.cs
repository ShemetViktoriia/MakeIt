using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MakeIt.Repository.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);

        TEntity Get(int id);
        IEnumerable<TType> Get<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class;
        IEnumerable<TType> Get<TType>(Expression<Func<TEntity, TType>> select) where TType : class;
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Edit(TEntity entity);
        void Edit(IEnumerable<TEntity> entities);

        void Delete(int id);
        void Delete(TEntity entity);
    }
}

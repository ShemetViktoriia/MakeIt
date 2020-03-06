using AutoMapper;
using MakeIt.BLL.DataTables;
using MakeIt.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MakeIt.BLL.Common
{
    public abstract class EntityService<T> : IEntityService<T>, IDataTablesRepository<T>  where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public EntityService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _unitOfWork.GetRepository<T>().Add(entity);
            _unitOfWork.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.GetRepository<T>().Edit(entity);
            _unitOfWork.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.GetRepository<T>().Delete(entity);
            _unitOfWork.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _unitOfWork.GetRepository<T>().GetAll();
        }

        #region DataTables Repository
        public async Task<IEnumerable<T>> GetPagedSortedFilteredListAsync(int start, int length, string orderColumnName, ListSortDirection order, string searchValue)
        {
            return await CreateQueryWithWhereAndOrderBy(searchValue, orderColumnName, order)
                .Skip(start)
                .Take(length)
                .ToListAsync();
        }

        public async Task<int> GetRecordsFilteredAsync(string searchValue)
        {
            IQueryable<T> query = _unitOfWork.GetRepository<T>().GetQuryableAll();

            return await GetWhereQueryForSearchValue(query, searchValue).CountAsync();
        }

        public async Task<int> GetRecordsTotalAsync()
        {
            IQueryable<T> query = _unitOfWork.GetRepository<T>().GetQuryableAll();

            return await query.CountAsync();
        }

        public virtual string GetSearchPropertyName()
        {
            return null;
        }

        protected virtual IQueryable<T> CreateQueryWithWhereAndOrderBy(string searchValue, string orderColumnName, ListSortDirection order)
        {
            IQueryable<T> query = _unitOfWork.GetRepository<T>().GetQuryableAll();

            query = GetWhereQueryForSearchValue(query, searchValue);

            query = AddOrderByToQuery(query, orderColumnName, order);

            return query;
        }

        /// <summary>
        /// This generic implementation relies on there being a SearchProperty on the Entity which 
        /// contains a concatenation of the data being displayed.
        /// Override for specific Datatables to handle logic such as searching on formated dates
        /// and concatenations.
        /// </summary>
        /// <param name="queryable">Entity framework query object</param>
        /// <param name="searchValue">text string to search on all displayed columns</param>
        /// <returns></returns>
        protected virtual IQueryable<T> GetWhereQueryForSearchValue(IQueryable<T> queryable, string searchValue)
        {
            string searchPropertyName = GetSearchPropertyName();

            if (!string.IsNullOrWhiteSpace(searchValue) && !string.IsNullOrWhiteSpace(searchPropertyName))
            {
                var searchValues = Regex.Split(searchValue, "\\s+");

                foreach (string value in searchValues)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        queryable = queryable.Where(GetExpressionForPropertyContains(searchPropertyName, value));
                    }
                }

                return queryable;
            }

            return queryable;
        }

        /// <summary>
        /// This generic implementation of adding the OrderBy clauses to the query will not
        /// handle any properties which do not exist on the DB table, such as concatenations
        /// or formatted dates.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="orderColumnName"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        protected virtual IQueryable<T> AddOrderByToQuery(IQueryable<T> query, string orderColumnName, ListSortDirection order)
        {
            var orderDirectionMethod = order == ListSortDirection.Ascending
                    ? "OrderBy"
                    : "OrderByDescending";

            var type = typeof(T);
            var property = type.GetProperty(orderColumnName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var filteredAndOrderedQuery = Expression.Call(typeof(Queryable), orderDirectionMethod, new Type[] { type, property.PropertyType }, query.Expression, Expression.Quote(orderByExp));

            return query.Provider.CreateQuery<T>(filteredAndOrderedQuery);
        }

        protected Expression<Func<T, bool>> GetExpressionForPropertyContains(string propertyName, string value)
        {
            var parent = Expression.Parameter(typeof(T));
            MethodInfo method = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
            var expressionBody = Expression.Call(Expression.Property(parent, propertyName), method, Expression.Constant(value));
            return Expression.Lambda<Func<T, bool>>(expressionBody, parent);
        }
        #endregion
    }
}

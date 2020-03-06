using MakeIt.BLL.DataTables;
using System.Collections.Generic;

namespace MakeIt.BLL.Common
{
    public interface IEntityService<T> : IService, IDataTablesRepository<T> where T : class
    {
        void Create(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}

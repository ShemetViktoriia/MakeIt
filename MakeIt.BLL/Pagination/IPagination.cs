using MakeIt.EF.Common;
using System.Collections.Generic;
using System.Linq;

namespace MakeIt.BLL.Pagination
{
    public interface IPagination<T> where T: class
    {
        /// <summary>
        /// Get data paginated
        /// </summary>
        /// <param name="filter">Filter applied by user</param>
        /// <param name="initialPage">Initial page</param>
        /// <param name="pageSize">Amount of records for each page</param>
        /// <param name="totalRecords">Total of records</param>
        /// <param name="recordsFiltered">Total records filtered when filter applied</param>
        /// <returns>List of all categories found</returns>
        IEnumerable<T> GetPaginated(string filter, int initialPage, int pageSize, out int totalRecords, out int recordsFiltered);
    }
}

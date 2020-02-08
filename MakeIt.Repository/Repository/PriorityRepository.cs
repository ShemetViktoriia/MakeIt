using MakeIt.EF;
using MakeIt.Repository.GenericRepository;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public interface IPriorityRepository : IGenericRepository<Priority>
    {
    }
    public class PriorityRepository : GenericRepository<Priority>, IPriorityRepository
    {
        public PriorityRepository(DbContext context)
            : base(context)
        {

        }
    }
}

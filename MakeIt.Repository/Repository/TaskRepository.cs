using MakeIt.EF;
using MakeIt.Repository.GenericRepository;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public interface ITaskRepository : IGenericRepository<Task>
    {
    }
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        public TaskRepository(DbContext context)
            : base(context)
        {

        }
    }
}

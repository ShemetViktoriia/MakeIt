using MakeIt.EF;
using MakeIt.Repository.GenericRepository;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
    }
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext context)
            : base(context)
        {

        }
    }
}

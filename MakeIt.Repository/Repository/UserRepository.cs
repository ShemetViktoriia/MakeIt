using MakeIt.EF;
using MakeIt.Repository.GenericRepository;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {

        }
    }
}

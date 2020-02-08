using MakeIt.EF;
using MakeIt.Repository.GenericRepository;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public interface IColorRepository : IGenericRepository<Color>
    {
    }

    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {
        public ColorRepository(DbContext context)
            : base(context)
        {

        }
    }
}

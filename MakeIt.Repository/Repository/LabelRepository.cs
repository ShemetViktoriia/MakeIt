using MakeIt.EF;
using MakeIt.Repository.GenericRepository;
using System.Data.Entity;

namespace MakeIt.Repository.Repository
{
    public interface ILabelRepository : IGenericRepository<Label>
    {
    }
    public class LabelRepository : GenericRepository<Label>, ILabelRepository
    {
        public LabelRepository(DbContext context)
            : base(context)
        {

        }
    }
}

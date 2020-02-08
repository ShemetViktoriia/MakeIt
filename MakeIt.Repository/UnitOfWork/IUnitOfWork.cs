using MakeIt.Repository.GenericRepository;
using MakeIt.Repository.Repository;
using System;

namespace MakeIt.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;

        IColorRepository Colors { get; }
        ICommentRepository Comments { get; }
        ILabelRepository Labels { get; }
        IMilestoneRepository Milestones { get; }
        IPriorityRepository Priorities { get; }
        IProjectRepository Projects { get; }
        IStatusRepository Statuses { get; }
        ITaskRepository Tasks { get; }
        IUserRepository Users { get; }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int SaveChanges();
    }
}

using MakeIt.EF;
using MakeIt.Repository.GenericRepository;
using MakeIt.Repository.Repository;
using System;
using System.Collections.Generic;

namespace MakeIt.Repository.UnitOfWork
{
    /// <summary>
    /// The Entity Framework implementation of IUnitOfWork
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        public MakeItContext _dbContext = new MakeItContext();

        private Dictionary<string, object> _repositories;

        public IColorRepository Colors { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public ILabelRepository Labels { get; private set; }
        public IMilestoneRepository Milestones { get; private set; }
        public IPriorityRepository Priorities { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public IStatusRepository Statuses { get; private set; }
        public ITaskRepository Tasks { get; private set; }
        public IUserRepository Users { get; private set; }

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">The object context</param>
        public UnitOfWork()
        {
            _repositories = new Dictionary<string, object>();
            Colors = new ColorRepository(_dbContext);
            Comments = new CommentRepository(_dbContext);
            Labels = new LabelRepository(_dbContext);
            Milestones = new MilestoneRepository(_dbContext);
            Priorities = new PriorityRepository(_dbContext);
            Projects = new ProjectRepository(_dbContext);
            Statuses = new StatusRepository(_dbContext);
            Tasks = new TaskRepository(_dbContext);
            Users = new UserRepository(_dbContext);
        }

        /// <summary>
        /// Search for repository in dictionary and if not exists creating new.
        /// </summary>
        /// <typeparam name="T">Type of repository to create.</typeparam>
        /// <returns>Returns repository with DbContext provided by UoW.</returns>
        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);
                _repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepository<T>)_repositories[type];
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public int SaveChanges()
        {
            // Save changes with the default options
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}

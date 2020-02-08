using AutoMapper;
using MakeIt.Repository.UnitOfWork;
using System;
using System.Collections.Generic;

namespace MakeIt.BLL.Common
{
    public abstract class EntityService<T> : IEntityService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public EntityService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _unitOfWork.GetRepository<T>().Add(entity);
            _unitOfWork.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.GetRepository<T>().Edit(entity);
            _unitOfWork.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.GetRepository<T>().Delete(entity);
            _unitOfWork.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _unitOfWork.GetRepository<T>().GetAll();
        }
    }
}

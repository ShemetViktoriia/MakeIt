using AutoMapper;
using MakeIt.BLL.Common;
using MakeIt.BLL.DTO;
using MakeIt.EF;
using MakeIt.Repository.UnitOfWork;
using System.Collections.Generic;

namespace MakeIt.BLL.Service.TaskOperations
{
    public interface ITaskService : IEntityService<Project>
    {
        IEnumerable<TaskDTO> GetUserTasksById(int userId);
        TaskDTO CreateTask(TaskDTO task, int ownerId);
        TaskDTO EditTask(TaskDTO task);
    }

    public class TaskService : EntityService<Project>, ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IMapper mapper, IUnitOfWork uow)
            : base(mapper, uow)
        {
            _unitOfWork = uow;
        }

        public TaskDTO CreateTask(TaskDTO task, int ownerId)
        {
            var taskAdded = new Task
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = _unitOfWork.GetRepository<Priority>().SingleOrDefault(p => p.Name.ToUpper().Equals(task.Priority.ToUpper())),
                Status = _unitOfWork.GetRepository<Status>().SingleOrDefault(s => s.Name.ToUpper().Equals(task.Status.ToUpper())),
                Project = _unitOfWork.GetRepository<Project>().SingleOrDefault(pr => pr.Name.ToUpper().Equals(task.Project.ToUpper())),
                //AssignedUser = _unitOfWork.GetRepository<User>().SingleOrDefault(au => au.UserName.ToUpper().Equals(task.AssignedUser.ToUpper())),
                //CreatedUser = _unitOfWork.GetRepository<User>().Get(ownerId)
            };
            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Task>().Add(taskAdded);
                _unitOfWork.SaveChanges();
            };
            return _mapper.Map<TaskDTO>(taskAdded);
        }

        public TaskDTO EditTask(TaskDTO task)
        {
            var taskEdited = _unitOfWork.GetRepository<Task>().Get(task.Id);
            taskEdited.Title = task.Title;
            taskEdited.Description = task.Description;
            taskEdited.DueDate = task.DueDate;
            taskEdited.Priority = _unitOfWork.GetRepository<Priority>().SingleOrDefault(p => p.Name.ToUpper().Equals(task.Priority.ToUpper()));
            taskEdited.Status = _unitOfWork.GetRepository<Status>().SingleOrDefault(s => s.Name.ToUpper().Equals(task.Status.ToUpper()));
            taskEdited.Project = _unitOfWork.GetRepository<Project>().SingleOrDefault(pr => pr.Name.ToUpper().Equals(task.Project.ToUpper()));
            //taskEdited.AssignedUser = _unitOfWork.GetRepository<User>().SingleOrDefault(au => au.UserName.ToUpper().Equals(task.AssignedUser.ToUpper()));

            using (_unitOfWork)
            {
                _unitOfWork.GetRepository<Task>().Edit(taskEdited);
                _unitOfWork.SaveChanges();
            };
            return _mapper.Map<TaskDTO>(taskEdited);
        }

        public IEnumerable<TaskDTO> GetUserTasksById(int userId)
        {
            var taskList = _unitOfWork.GetRepository<Task>().Find(t => t.AssignedUserId == userId);
            return _mapper.Map<IEnumerable<TaskDTO>>(taskList);
        }
    }
}

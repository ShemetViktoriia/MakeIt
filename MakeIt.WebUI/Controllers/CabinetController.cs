using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Service.TaskOperations;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MakeIt.WebUI.ViewModel;
using Microsoft.AspNet.Identity;
using MakeIt.EF;
using MakeIt.Repository.UnitOfWork;
using MakeIt.WebUI.Filters;

namespace MakeIt.WebUI.Controllers
{
    [AuthenticationFilter]
    public class CabinetController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITaskService _tasktService;

        public CabinetController(IMapper mapper, ITaskService taskService, IUnitOfWork unitOfWork) : base(mapper)
        {
            _unitOfWork = unitOfWork;
            _tasktService = taskService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            int userId = User.Identity.GetUserId<int>();
          
            var tasks = _unitOfWork.Tasks.GetAll().ToList().Where(t => t.AssignedUserId == userId);
           
            var tempTasks = new List<TaskShowViewModel>();
            foreach (var task in tasks)
            {
                if (task.Status.Name.ToUpper().Equals("closed".ToUpper()))
                    continue;
                TaskShowViewModel currentTask = new TaskShowViewModel();
                currentTask.Id = task.Id;
                currentTask.Title = task.Title;
                currentTask.Description = task.Description;
                currentTask.Priority = task.Priority.Name;
                currentTask.Project = task.Project.Name;
                currentTask.Status = task.Status.Name;
                currentTask.DueDate = task.DueDate;
                //currentTask.AssignedUser = task.AssignedUser.UserName;
                //currentTask.CreatedUser = task.CreatedUser.UserName;
                tempTasks.Add(currentTask);
            }

            var sortedUserTasksList = (from task in tempTasks
                                      orderby task.DueDate descending
                                      select task).ToList();
            return View(sortedUserTasksList);
        }
    }
}
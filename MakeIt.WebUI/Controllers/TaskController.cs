using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Service.TaskOperations;
using MakeIt.EF;
using MakeIt.Repository.UnitOfWork;
using MakeIt.WebUI.Filters;
using MakeIt.WebUI.Response;
using MakeIt.WebUI.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MakeIt.WebUI.Controllers
{
    [AuthenticationFilter]
    public class TaskController : BaseController
    {
        private readonly ITaskService _taskService;
        private IUnitOfWork _unitOfWork;
        public TaskController(IMapper mapper, ITaskService taskService, IUnitOfWork unitOfWork) : base(mapper)
        {
            _taskService = taskService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> TaskListViewData(int draw, int start, int length)
        {
            var userId = User.Identity.GetUserId<int>();

            // get the search string
            var searchString = Request.QueryString["search[value]"];

            var recordsTotal = await _taskService.GetRecordsTotalAsync();
            //var recordsFiltered = await _taskService.GetRecordsFilteredAsync(searchString);

            // to create with use stored procedure
            // analogy to
            // GetPagedSortedFilteredListAsync(start, length, orderColumnName, orderDirection, searchString)

            var listTasks = new List<GetTaskSelectedPage_sp_Result>();
            using (var db = new MakeItContext())
            {
                listTasks = db.Database.SqlQuery<GetTaskSelectedPage_sp_Result>("GetTaskSelectedPage_sp @SearchValue, @PageNo, @PageSize, @SortColumn, @SortOrder, @AssignedUserId",
                    new SqlParameter("@SearchValue", searchString),
                    new SqlParameter("@PageNo", start),
                    new SqlParameter("@PageSize", length),
                    new SqlParameter("@SortColumn", "Id"),
                    new SqlParameter("@SortOrder", "Asc"),
                    new SqlParameter("@AssignedUserId", userId)).ToList();
            }
            var recordsFiltered = listTasks.Any()? listTasks[0].RecordsTotal ?? 0 : 0;
            var response = new DataTablesResponse<GetTaskSelectedPage_sp_Result>()
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = listTasks
            };

            // serialize response object to json string
            var jsonResponse = Json(response, JsonRequestBehavior.AllowGet);

            return jsonResponse;
        }

        [HttpGet]
        public ActionResult Create()
        {
            string currentUser = "";
            if (User.Identity.IsAuthenticated)
            {
                currentUser = User.Identity.Name;
            }
            else
            {
                return RedirectToAction("LogOff", "Account");
            }
            var currentUserobj = _unitOfWork.Users.GetAll()
                .First(us => us.UserName.ToUpper().Equals(currentUser.ToUpper()));

            //var projects = _unitOfWork.Projects.GetAll().ToList().Where(pr => pr.Owner == currentUserobj || pr.Members.Contains(currentUserobj));
            //var projNames = from proj in projects select proj.Name;
            //SelectList project = new SelectList(projNames, "Project");

            //ViewBag.Project = project;


            var prior = _unitOfWork.Priorities.GetAll();
            var prName = from p in prior select p.Name;
            SelectList priority = new SelectList(prName, "Priority");
            ViewBag.Priority = priority;

            var stone = _unitOfWork.Milestones.GetAll();
            var stoneNames = from s in stone select s.Title;
            SelectList milestones = new SelectList(stoneNames, "Milestone");
            ViewBag.Milestone = milestones;

            var tu = _unitOfWork.Users.GetAll().ToList();


            var users = _unitOfWork.Users.GetAll();
            var userNames = from u in users select u.UserName;
            SelectList usersNames = new SelectList(userNames, "AssignedUser");
            ViewBag.Users = usersNames;

            ViewBag.User = currentUser;
            return View();
        }

        [HttpPost]
        public ActionResult Save(TaskShowViewModel newTask)
        {
            var userId = User.Identity.GetUserId<int>();
            var userName = User.Identity.Name;
            int taskId = newTask.Id ?? default(int);

            var oldTask = _unitOfWork.Tasks.Get(taskId);

            var model = new TaskShowViewModel();
            if (newTask.Id == null)
            {
                model = new TaskShowViewModel
                {
                    Title = newTask.Title,
                    Description = newTask.Description,
                    Priority = newTask.Priority,
                    Status = "Open",
                    Project = newTask.Project,
                    AssignedUser = newTask.AssignedUser,
                    CreatedUser = userName,
                    DueDate = newTask.DueDate
                };
            }
            else
            {
                model.Id = newTask.Id;
                model.Title = newTask.Title.Equals(oldTask.Title) ? oldTask.Title : newTask.Title;
                model.Description = newTask.Description.Equals(oldTask.Description) ? oldTask.Description : newTask.Description;
                model.Status = newTask.Status != null && !newTask.Status.Equals(oldTask.Status.Name) ?
                    newTask.Status : oldTask.Status.Name;
                model.Priority = newTask.Priority != null && !newTask.Priority.Equals(oldTask.Priority.Name) ?
                    newTask.Priority : oldTask.Priority.Name;
                model.Project = newTask.Project != null && !newTask.Project.Equals(oldTask.Project.Name) ?
                    newTask.Project : oldTask.Project.Name;
                //model.AssignedUser = newTask.AssignedUser != null && newTask.AssignedUser.Equals(oldTask.AssignedUser.UserName) ?
                    //newTask.AssignedUser : oldTask.AssignedUser.UserName;
                //model.CreatedUser = oldTask.CreatedUser.UserName;
                model.DueDate = newTask.DueDate != null && newTask.DueDate > DateTime.MinValue && !newTask.DueDate.ToString("mm/dd/yyyy").Equals(oldTask.DueDate.ToString("mm/dd/yyyy")) ?
                    newTask.DueDate : oldTask.DueDate;

            }
            var taskDTO = _mapper.Map<TaskDTO>(model);
            if (model.Id == null)
                taskDTO = _taskService.CreateTask(taskDTO, userId);
            else taskDTO = _taskService.EditTask(taskDTO);

            return RedirectToAction("Index", "Cabinet");
        }

        [HttpGet]
        public ActionResult Show(int taskId)
        {
            var task = _unitOfWork.Tasks.Get(taskId);
            TaskShowViewModel currentTask = new TaskShowViewModel();

            currentTask.Id = task.Id;
            currentTask.Title = task.Title;
            currentTask.Description = task.Description;
            currentTask.Priority = task.Priority.Name;
            currentTask.Status = task.Status.Name;
            currentTask.Project = task.Project.Name;
            //currentTask.AssignedUser = task.AssignedUser.UserName;
            //currentTask.CreatedUser = task.CreatedUser.UserName;
            currentTask.DueDate = task.DueDate;

            return View(currentTask);
        }

        [HttpGet]
        public ActionResult Edit(int? taskId, bool isNewTask = false)
        {
            var task = _unitOfWork.Tasks.Get(taskId.Value);

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

            string currentUser = User.Identity.Name;
            var currentUserobj = _unitOfWork.Users.GetAll()
                .First(us => us.UserName.ToUpper().Equals(currentUser.ToUpper()));

            //var projects = _unitOfWork.Projects.GetAll().ToList().Where(pr => pr.Owner == currentUserobj || pr.Members.Contains(currentUserobj));
            //var projNames = from proj in projects select proj.Name;
            //ViewBag.Project = new SelectList(projNames, "Project");


            var priority = new SelectList(_unitOfWork.Priorities.GetAll(), "Name", "Name");
            ViewBag.Priority = priority;

            var statuses = new SelectList(_unitOfWork.Statuses.GetAll(), "Name", "Name");
            ViewBag.Status = statuses;

            var usersForAssignee = new SelectList(_unitOfWork.Users.GetAll(), "UserName", "UserName");
            ViewBag.UsersForAssignee = usersForAssignee;

            return View(currentTask);
        }
    }
}
using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Service.Authorithation;
using MakeIt.BLL.Service.ProjectOperations;
using MakeIt.WebUI.Filters;
using MakeIt.WebUI.Response;
using MakeIt.WebUI.SignalR.Hubs;
using MakeIt.WebUI.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MakeIt.WebUI.Controllers
{
    [AuthenticationFilter]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IAuthorizationService _authorizationService;
        public ProjectController(IMapper mapper, IProjectService projectService, IAuthorizationService authorizationService) : base(mapper)
        {
            _projectService = projectService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns JQuery Datatable JSON for server-side processing, using a view in the DB to improve performance 
        /// and simplify logic needed for searching/sorting. This allows generic implementation of a full word search
        /// on the DB against all columns.
        /// </summary>
        /// <param name="draw">pass this back unchanged in the response</param>
        /// <param name="start">number of records to skip</param>
        /// <param name="length">number of records to return</param>
        /// <returns>JSON for Datatables response</returns>
        public async Task<JsonResult> ProjectListViewData(int draw, int start, int length)
        {
            // get the column index of datatable to sort on
            var orderByColumnNumber = Convert.ToInt32(Request.QueryString["order[0][column]"]);
            var orderColumnName = GetProjectListColumnName(orderByColumnNumber);

            // get direction of sort
            var orderDirection = Request.QueryString["order[0][dir]"] == "asc"
                ? ListSortDirection.Ascending
                : ListSortDirection.Descending;

            // get the search string
            var searchString = Request.QueryString["search[value]"];


            var recordsTotal = await _projectService.GetRecordsTotalAsync();
            var recordsFiltered = await _projectService.GetRecordsFilteredAsync(searchString);
            var projectListDTO = _mapper.Map<IEnumerable<ProjectDTO>>(await _projectService.GetPagedSortedFilteredListAsync(start, length, orderColumnName, orderDirection, searchString));
            var data = _mapper.Map<IEnumerable<ProjectViewModel>>(projectListDTO);

            var response = new DataTablesResponse<ProjectViewModel>()
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = data
            };

            // serialize response object to json string
            var jsonResponse = Json(response, JsonRequestBehavior.AllowGet);

            return jsonResponse;
        }

        private string GetProjectListColumnName(int columnNumber)
        {
            switch (columnNumber)
            {
                case 0:
                    return "Id";

                case 1:
                    return "Name";

                case 2:
                    return "Description";

                case 3:
                    return "LastUpdateDate";
            }
            return string.Empty;
        }

        [HttpGet]
        public ActionResult Edit(int? projectId, bool isNewProject = false)
        {
            if (TempData["Message"]!=null)
            {
                ViewBag.SuccessMsg = TempData["Message"];
            }

            int userId = User.Identity.GetUserId<int>();
            ViewBag.ActionDetermination = isNewProject ? "Create" : "Edit";
            if (isNewProject)
            {
                return View(new ProjectViewModel { RoleInProject = BLL.Enum.RoleInProjectEnum.Owner});
            }
            var projectDTO = _projectService.GetProjectById(projectId.Value);
            projectDTO.RoleInProject = projectDTO.Owner.Id == userId ?
                                        BLL.Enum.RoleInProjectEnum.Owner :
                                        BLL.Enum.RoleInProjectEnum.Member;

            var projectViewModel = _mapper.Map<ProjectViewModel>(projectDTO);
            return View(projectViewModel);
        }

        [HttpPost]
        public ActionResult EditProject(ProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            ViewBag.ActionResult = model.Id == null ? "Just created new" : "Just edited";
            ViewBag.ActionDetermination = "Edit";

            int userId = User.Identity.GetUserId<int>();
            var projectDTO = _mapper.Map<ProjectDTO>(model);

            var projectEditedDTO = new ProjectDTO();     
            if (model.Id == null)
                projectEditedDTO = _projectService.CreateProject(projectDTO, userId);
            else projectEditedDTO = _projectService.EditProject(projectDTO);

            var projectViewModel = _mapper.Map<ProjectViewModel>(projectEditedDTO);
            
            return View("Edit", projectViewModel);
        }

        public async Task<ActionResult> ConfirmInvite(int userId, int projectId, string code)
        {
            var userDTO = await _authorizationService.FindByIdAsync(userId.ToString());
            if (_authorizationService.IsTokenExpired(userDTO, code))
            {
                return View("CrashedLink");
            }
            var projectViewModel = _mapper.Map<ProjectViewModel>(_projectService.GetProjectById(projectId));
            projectViewModel.RoleInProject = BLL.Enum.RoleInProjectEnum.Member;

            ViewBag.ActionDetermination = "Edit";
            if (_authorizationService.IsProjectMember(userId, projectId))
            {
                ViewBag.ActionResult = "You are already a member of this project";
                return View("Edit", projectViewModel);
            }
            var projectAddedViewModel = _mapper.Map<ProjectViewModel>(_projectService.AddProjectMember(userId, projectId));
            projectAddedViewModel.RoleInProject = BLL.Enum.RoleInProjectEnum.Member;

            // Получаем контекст хаба
            var context =
                Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<InviteNotificationHub>();

            var ss =  context.Clients.All as List<string>;
            // отправляем сообщение
            context.Clients.All.displayMessage(userDTO.UserName +" joined to #" + projectId + " project");

            ViewBag.ActionResult = "You have joined to project just now";
            return View("Edit", projectAddedViewModel);
        }
    }
}
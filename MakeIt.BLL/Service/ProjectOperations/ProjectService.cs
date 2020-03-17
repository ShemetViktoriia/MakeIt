using AutoMapper;
using MakeIt.BLL.Common;
using MakeIt.BLL.DTO;
using MakeIt.BLL.Enum;
using MakeIt.EF;
using MakeIt.Repository.UnitOfWork;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;

namespace MakeIt.BLL.Service.ProjectOperations
{
    public interface IProjectService : IEntityService<Project>
    {
        IEnumerable<ProjectDTO> GetUserProjectsById(int userId);
        ProjectDTO GetProjectById(int projectId);
        ProjectDTO CreateProject(ProjectDTO project, int ownerId);
        ProjectDTO EditProject(ProjectDTO project);
        ProjectDTO AddProjectMember(int userId, int projectId);
        IEnumerable<string> GetNamesProjectMembers(int projectId);
    }

    public class ProjectService : EntityService<Project>, IProjectService
    {
        private IUnitOfWork _unitOfWork;

        public ProjectService(IMapper mapper, IUnitOfWork uow)
            : base(mapper, uow)
        {
            _unitOfWork = uow;
        }

        public ProjectDTO AddProjectMember(int userId, int projectId)
        {
            var project = _unitOfWork.Projects.Get(projectId);
            var user = _unitOfWork.Users.Get(userId);
            project.Users.Add(user);
            _unitOfWork.SaveChanges();
            return _mapper.Map<ProjectDTO>(project);
        }

        public ProjectDTO CreateProject(ProjectDTO project, int ownerId)
        {
            var projectAdded = new Project
            {
                Name = project.Name,
                Description = project.Description,
                User = _unitOfWork.GetRepository<User>().Get(ownerId)
            };
            _unitOfWork.GetRepository<Project>().Add(projectAdded);
            _unitOfWork.SaveChanges();
            return _mapper.Map<ProjectDTO>(projectAdded);
        }

        public ProjectDTO EditProject(ProjectDTO project)
        {
            var projectEdited = _unitOfWork.GetRepository<Project>().Get(project.Id);
            projectEdited.Name = project.Name;
            projectEdited.Description = project.Description;
            _unitOfWork.GetRepository<Project>().Edit(projectEdited);
            _unitOfWork.SaveChanges();
            return _mapper.Map<ProjectDTO>(projectEdited);
        }

        public IEnumerable<string> GetNamesProjectMembers(int projectId)
        {
            var project = _unitOfWork.GetRepository<Project>().Get(projectId);
            var nameOwnerList = string.Empty;//project.OwnerId.UserName;
            var namesMemberList = project.Users.Select(m => m.UserName).ToList();
            namesMemberList.Add(nameOwnerList);
            return namesMemberList;
        }

        public ProjectDTO GetProjectById(int projectId)
        {
            var project = _unitOfWork.GetRepository<Project>().Get(projectId);
            return _mapper.Map<ProjectDTO>(project);
        }

        public IEnumerable<ProjectDTO> GetUserProjectsById(int userId)
        {
            // Owner projects
            var projectOwnerList = _unitOfWork.GetRepository<Project>()
                .Find(p => p.OwnerId == userId).ToList();
            var projectOwnerDTOList = _mapper.Map<IEnumerable<ProjectDTO>>(projectOwnerList);
            projectOwnerDTOList.ToList().ForEach(p => p.RoleInProject = RoleInProjectEnum.Owner);

            // Member projects
            var projectMemberList = _unitOfWork.GetRepository<Project>()
                .Find(p=>p.Users.Where(m => m.Id == userId).Any()).ToList();
            var projectMemberDTOList = _mapper.Map<IEnumerable<ProjectDTO>>(projectMemberList);
            projectMemberDTOList.ToList().ForEach(p => p.RoleInProject = RoleInProjectEnum.Member);

            return projectOwnerDTOList.Union(projectMemberDTOList);
        }

        protected override IQueryable<Project> GetWhereQueryForSearchValue(IQueryable<Project> queryable, string searchValue)
        {
            return queryable.Where(x =>
                    // id column (int)
                    SqlFunctions.StringConvert((double)x.Id).Contains(searchValue)
                    // name column (string)
                    || x.Name.Contains(searchValue)
                    // description column (string)
                    || x.Description.Contains(searchValue)
                    // date (datetime, formatted as d/M/yyyy) - limitation of sql prevented us from getting leading zeros in day or month
                    || (SqlFunctions.StringConvert((double)SqlFunctions.DatePart("dd", x.CreatedDate)) + "/" + SqlFunctions.DatePart("mm", x.CreatedDate) + "/" + SqlFunctions.DatePart("yyyy", x.CreatedDate)).Contains(searchValue));
        }
    }
}

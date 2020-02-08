using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.WebUI.ViewModel;

namespace MakeIt.WebUI.AutoMapper
{
    public class DTOToViewModelMappingProfile : Profile
    {
        public DTOToViewModelMappingProfile()
        {
            // create mapping           
            CreateMap<UserAuthDTO, LoginViewModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.RememberMe, opt => opt.MapFrom(src => src.RememberMe))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<MemberDTO, MemberViewModel>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
              .ForAllOtherMembers(x => x.Ignore());

            CreateMap<UserAuthDTO, RegisterViewModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.ConfirmPassword, opt => opt.MapFrom(src => src.ConfirmPassword))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<ProjectDTO, ProjectViewModel>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.LastUpdateDate, opt => opt.MapFrom(src => src.UpdatedDate))
              .ForMember(dest => dest.IsClosed, opt => opt.MapFrom(src => src.IsClosed))
              .ForMember(dest => dest.RoleInProject, opt => opt.MapFrom(src => src.RoleInProject))
              .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Members))
              .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
              .ForAllOtherMembers(x => x.Ignore());

            CreateMap<TaskDTO, TaskShowViewModel>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))               
               .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
               .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
               .ForMember(dest => dest.AssignedUser, opt => opt.MapFrom(src => src.AssignedUser))
               .ForAllOtherMembers(x => x.Ignore());

            // TODO another maps for another issues
            // depending on the task
        }
    }
}
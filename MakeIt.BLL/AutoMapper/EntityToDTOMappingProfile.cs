using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.EF;

namespace MakeIt.BLL.AutoMapper
{
    public class EntityToDTOMappingProfile : Profile
    {
        public EntityToDTOMappingProfile()
        {
            CreateMap<User, UserAuthDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<User, MemberDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Project, ProjectDTO>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
               .ForMember(dest => dest.IsClosed, opt => opt.MapFrom(src => src.IsClosed))
               .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Users))
               .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.User))
               .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Task, TaskDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))                
                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project.Name))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                //.ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.CreatedUser.UserName))
                //.ForMember(dest => dest.AssignedUser, opt => opt.MapFrom(src => src.AssignedUser.UserName))
                .ForAllOtherMembers(x => x.Ignore());

            // TODO another maps for another issues
            // depending on the task
        }
    }
}

using AutoMapper;
using MakeIt.BLL.DTO;
using MakeIt.EF;
using System.Linq;

namespace MakeIt.BLL.AutoMapper
{
    public class DTOToEntityMappingProfile : Profile
    {
        public DTOToEntityMappingProfile()
        {
            CreateMap<UserAuthDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<MemberDTO, User>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
              .ForAllOtherMembers(x => x.Ignore());

            CreateMap<ProjectDTO, Project>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
                .ForMember(dest => dest.IsClosed, opt => opt.MapFrom(src => src.IsClosed))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Members))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Owner))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<TaskDTO, Task>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                /*.ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.AssignedUser, opt => opt.MapFrom(src => src.AssignedUser))*/
                .ForAllOtherMembers(x => x.Ignore());


            // TODO another maps for another issues
            // depending on the task
        }
    }
}

using AutoMapper;
using RolesAndUsers.Dtos;
using RolesAndUsers.Models;

namespace RolesAndUsers.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, src => src.MapFrom(src => src.UserRoles.Select(x => new RoleDto { Id = x.RoleId,Name = x.Role.Name})));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.UserRoles, src => src.MapFrom(src => src.Roles)).AfterMap((model, entity) =>
                {
                    foreach (var userRole in entity.UserRoles)
                    {
                        userRole.UserId = entity.Id;
                        userRole.User = entity;
                    }
                });

            CreateMap<RoleDto, UserRole>().ForMember(dest => dest.Role, src => src.MapFrom(src => src))
                .ForMember(dest => dest.RoleId, src => src.MapFrom(src => src.Id));

            CreateMap<Role, RoleDto>().ForSourceMember(x => x.UserRoles, y => y.DoNotValidate()).ForMember(dest => dest.Name, src => src.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.Id)).ReverseMap();
        }
    }
}

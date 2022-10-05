using AutoMapper;
using RolesAndUsers.Dtos;
using RolesAndUsers.Models;

namespace RolesAndUsers.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            /*CreateMap<UserDto, User>();*/

            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}

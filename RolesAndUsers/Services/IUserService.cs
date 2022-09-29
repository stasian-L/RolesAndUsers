using RolesAndUsers.Dtos;

namespace RolesAndUsers.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUsers();
    Task<UserDto> GetUser(Guid userId);
    Task<UserDto> AddUser(UserDto user);
    Task UpdateUser(Guid id, UserDto user);
    Task DeleteUser(Guid userId);
}

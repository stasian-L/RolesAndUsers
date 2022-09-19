using RolesAndUsers.Models;

namespace RolesAndUsers.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUser(Guid userId);
    Task<User> AddUser(User user);
    Task<User> UpdateUser(User user);
    Task DeleteUser(Guid userId);

}

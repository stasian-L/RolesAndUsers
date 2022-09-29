using RolesAndUsers.Models;

namespace RolesAndUsers.Repositories;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetRoles();
    Task<Role> GetRole(Guid roleId);
    Task<Role> AddRole(Role role);
    Task<Role> UpdateRole(Role role);
    Task DeleteRole(Guid roleId);
}

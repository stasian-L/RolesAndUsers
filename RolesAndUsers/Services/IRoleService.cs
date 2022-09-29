using RolesAndUsers.Dtos;

namespace RolesAndUsers.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetRoles();
    Task<RoleDto> GetRole(Guid roleId);
    Task<RoleDto> AddRole(RoleDto role);
    Task UpdateRole(Guid id, RoleDto role);
    Task DeleterRole(Guid roleId);
}

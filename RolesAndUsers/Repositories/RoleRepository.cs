using Microsoft.EntityFrameworkCore;
using RolesAndUsers.Data;
using RolesAndUsers.Models;

namespace RolesAndUsers.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }
     
    public async Task<Role> AddRole(Role role)
    {
        var result = await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task DeleterRole(Guid roleId)
    {
        var role = await _context.Roles.FindAsync(roleId);

        if (role != null)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Role> GetRole(Guid roleId)
    {
        return await _context.Roles.FindAsync(roleId);
    }

    public async Task<IEnumerable<Role>> GetRoles()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Role> UpdateRole(Role role)
    {
        var result = await _context.Roles
                .FirstOrDefaultAsync(e => e.Id == role.Id);

        if (result != null)
        {
            result.Name = role.Name;

            await _context.SaveChangesAsync();

            return result;
        }

        return null;
    }
}

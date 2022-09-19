using Microsoft.EntityFrameworkCore;
using RolesAndUsers.Data;
using RolesAndUsers.Models;

namespace RolesAndUsers.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddUser(User user)
    {
        var roles = _context.Roles
            .Where(x => user.UserRoles
            .Select(x => x.RoleId).Contains(x.Id))
            .ToList();

        user.UserRoles = roles
            .Select(x => new UserRole { Role = x, RoleId = x.Id })
            .ToList();

        var result = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task DeleteUser(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<User> GetUser(Guid userId)
    {
        return await _context.Users
            .Include(b => b.UserRoles)
            .ThenInclude(u => u.Role)
            .FirstOrDefaultAsync(i => i.Id == userId);
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users.Include(u => u.UserRoles).ThenInclude(u => u.Role).ToListAsync();
    }

    public async Task<User> UpdateUser(User user)
    {
        var result = await _context.Users
                .FirstOrDefaultAsync(e => e.Id == user.Id);

        var roles = _context.Roles.Where(x => user.UserRoles.Select(x => x.RoleId).Contains(x.Id)).ToList();

        if (result != null)
        {
            result.Name = user.Name;
            result.UserRoles = roles
                .Select(x => new UserRole { Role = x, RoleId = x.Id, User = user, UserId = user.Id })
                .ToList();

            await _context.SaveChangesAsync();

            return result;
        }

        return null;
    }
}

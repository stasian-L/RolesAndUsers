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
            .Where(x => user.Roles
            .Select(x => x.Id).Contains(x.Id))
            .ToList();

        User newUser = new User
        {
            Id = user.Id,
            Name = user.Name,
            Roles = roles,
        };

        var result = await _context.Users.AddAsync(newUser);
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
            .Include(b => b.Roles)
            .FirstOrDefaultAsync(i => i.Id == userId);
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users.Include(u => u.Roles).ToListAsync();
    }

    public async Task<User> UpdateUser(User user)
    {
        var result = await _context.Users.Include(x => x.Roles)
                .FirstOrDefaultAsync(e => e.Id == user.Id);

        if(result != null)
        {
            result.Name = user.Name;
            _context.Entry(result).CurrentValues.SetValues(user);

            var userRoles = result.Roles.ToList();
            foreach (var userRole in userRoles)
            {
                var contact = user.Roles.SingleOrDefault(i => i.Id == userRole.Id);
                if (contact != null)
                    _context.Entry(userRole).CurrentValues.SetValues(contact);
                else
                    _context.Remove(userRole);
            }

            foreach (var contact in user.Roles)
            {
                if (userRoles.All(i => i.Id != contact.Id))
                {
                    result.Roles.Add(contact);
                }
            }

            await _context.SaveChangesAsync();

            return result;
        }

        return null;
    }

    private bool IsDetached(User entity)
    {
        var localEntity = _context.Users?.FirstOrDefault(x => Equals(x.Id, entity.Id));
        if (localEntity != null) // entity stored in local
            return false;

        return _context.Entry(entity).State == EntityState.Detached;
    }
}

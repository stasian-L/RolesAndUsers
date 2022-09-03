using Microsoft.EntityFrameworkCore;
using RolesAndUsers.Models;

namespace RolesAndUsers.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Define relationship between books and authors
            builder.Entity<User>()
                .HasMany(x => x.Roles);
                
            
        }
    }
}

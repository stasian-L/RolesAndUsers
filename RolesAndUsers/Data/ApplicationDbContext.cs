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
           builder.Entity<User>()
                .HasMany(u => u.Roles).WithMany(r=>r.Users).UsingEntity(j=>j.ToTable("RoleUser"));
        }
    }
}

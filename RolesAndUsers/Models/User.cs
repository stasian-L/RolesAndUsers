using System.ComponentModel.DataAnnotations;

namespace RolesAndUsers.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<Role> Roles { get; set; }
}

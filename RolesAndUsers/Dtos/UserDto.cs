namespace RolesAndUsers.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<RoleDto> Roles { get; set; }
}

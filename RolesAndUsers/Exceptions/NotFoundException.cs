namespace RolesAndUsers.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name)
            : base($"Entity with id = \"{name}\" was not found.")
        {
        }
    }
}

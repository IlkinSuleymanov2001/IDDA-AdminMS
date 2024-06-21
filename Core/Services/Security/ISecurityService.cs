namespace Core.Services.Security
{
    public interface ISecurityService
    {
        string? GetUsername();

        IEnumerable<string> GetRoles();

        bool IsHaveRole(string roleName="ADMIN");
    }
}

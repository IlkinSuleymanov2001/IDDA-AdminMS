namespace Core.Services.Security
{
    public interface ISecurityService
    {
        string? GetUsername();

        IEnumerable<string> GetRoles();

        bool IsAdmin();
    }
}

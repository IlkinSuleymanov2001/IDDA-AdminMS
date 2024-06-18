using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public interface ISecurityService
    {
        string? GetUsername();

        IEnumerable<string> GetRoles();

        bool IsAdmin();
    }
}

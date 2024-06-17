using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public interface ISecurityService
    {
        string? GetUsername(IHttpContextAccessor httpContextAccessor);

        IEnumerable<string> GetRoles(IHttpContextAccessor httpContextAccessor);
    }
}

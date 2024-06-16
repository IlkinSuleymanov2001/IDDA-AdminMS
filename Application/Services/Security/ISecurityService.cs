using Microsoft.AspNetCore.Http;

namespace Application.Services.Abstracts
{
    public interface ISecurityService
    {
        string? GetUsername(IHttpContextAccessor httpContextAccessor);

        IEnumerable<string> GetRoles(IHttpContextAccessor httpContextAccessor);
    }
}

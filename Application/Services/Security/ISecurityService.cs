using Microsoft.AspNetCore.Http;

namespace Application.Services.Abstracts
{
    public interface ISecurityService
    {
        string? GetUsername(string token);

        IEnumerable<string> GetRoles(IHttpContextAccessor httpContextAccessor);
    }
}

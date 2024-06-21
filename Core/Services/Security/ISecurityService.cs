using Core.Pipelines.Authorization;

namespace Core.Services.Security
{
    public interface ISecurityService
    {
        string? GetUsername();

        IEnumerable<string> GetRoles();

        bool IsHaveRole(string roleName="ADMIN");

        void ValidateToken();

        void IsAccessToRequest(ISecuredRequest securedRequest);
    }
}

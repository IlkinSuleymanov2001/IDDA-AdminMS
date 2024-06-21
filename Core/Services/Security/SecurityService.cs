using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Services.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecurityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetUsername()
        {
            var token = GetToken();

            if (string.IsNullOrEmpty(token)) return default;
            return CheckTokenFormat(token).Claims?.Where(c => c.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value;
        }

        public IEnumerable<string> GetRoles()
        {

            var token = GetToken();
            var roles = CheckTokenFormat(token).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (roles.Any())
                return roles;
            else
                throw new UnAuthorizationException("your are not authorized");



        }

        private string GetToken()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"];

            if (authorizationHeader != StringValues.Empty)
            {
                string? jwtHeader = authorizationHeader.ToList().Where(c => c.Contains("Bearer")).FirstOrDefault();
                return jwtHeader != null ? jwtHeader.Split("Bearer").Last().Trim() : string.Empty;
            }
            throw new UnAuthorizationException("your are not authorized");
        }

        private JwtSecurityToken CheckTokenFormat(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken tokenData;

            try
            {
                tokenData = jwtHandler.ReadJwtToken(token);
            }
            catch (Exception)
            {
                throw new UnAuthorizationException("invalid token format ");
            }

            return tokenData;
        }



        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }

        public bool IsHaveRole(string roleName = "ADMIN")
        {
            return GetRoles().Any(c => c == roleName);
        }
    }
}

using Application.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Application.Services.Security
{
    public class SecurityService : ISecurityService
    {

        public string? GetUsername(IHttpContextAccessor httpContextAccessor)
        {
            var token = GetToken(httpContextAccessor);

            if (string.IsNullOrEmpty(token)) return default;
            return CheckTokenFormat(token).Claims?.Where(c => c.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value;
        }

        public IEnumerable<string> GetRoles(IHttpContextAccessor httpContextAccessor)
        {

            var token = GetToken(httpContextAccessor);
            var roles = CheckTokenFormat(token).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (roles.Any())
                return roles;
            else
                throw new UnauthorizedAccessException("your are not authorized");



        }

        private string GetToken(IHttpContextAccessor httpContextAccessor)
        {
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["authorization"];

            if (authorizationHeader != StringValues.Empty)
            {
                string? jwtHeader = authorizationHeader.ToList().Where(c => c.Contains("Bearer")).FirstOrDefault();
                return jwtHeader != null ? jwtHeader.Split("Bearer").Last().Trim() : string.Empty;
            }
            throw new UnauthorizedAccessException("your are not authorized");
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
                throw new UnauthorizedAccessException("invalid token format ");
            }

            return tokenData;
        }



        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}

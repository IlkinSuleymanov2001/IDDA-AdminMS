using Core.Exceptions;
using Core.Pipelines.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Core.Services.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly TokenOptions _tokenOptions;

        public SecurityService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public string? GetUsername()
        {
            var token = GetToken();

            if (string.IsNullOrEmpty(token)) return default;
            return CheckTokenFormat(token).Claims?.Where(c => c.Type ==Config.Username)?.FirstOrDefault()?.Value;
        }

        public IEnumerable<string> GetRoles()
        {

            var token = GetToken();
            var roles = CheckTokenFormat(token).Claims.Where(c => c.Type == Config.Roles).Select(c => c.Value).ToList();
            if (roles.Any())
                return roles;
            else
                throw new UnAuthorizationException();



        }

        private string GetToken()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"];

            if (authorizationHeader != StringValues.Empty)
            {
                string? jwtHeader = authorizationHeader.ToList().Where(c => c.Contains("Bearer")).FirstOrDefault();
                return jwtHeader != null ? jwtHeader.Split("Bearer").Last().Trim() : string.Empty;
            }
            throw new UnAuthorizationException();
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
                throw new BadRequestException("invalid token format");
            }

            return tokenData;
        }



        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }

        public bool CurrentRoleEqualsTo(string roleName = "ADMIN")
        {
            return GetRoles().Any(c => c == roleName);
        }


        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,

                ValidIssuer = _tokenOptions.Issuer,
                ValidAudience = _tokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = CreateSecurityKey(_tokenOptions.SecurityKey),
                LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                    expires != null ? expires > DateTime.UtcNow : false
            };
        }

        public void  ValidateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            try
            {
                // Validate token and set claimsPrincipal
                tokenHandler.ValidateToken(GetToken(), validationParameters, out SecurityToken validatedToken);
                return ;
            }
            catch (Exception)
            {
                throw new UnAuthorizationException();
            }
        }

        public void IsAccessToRequest(ISecuredRequest securedRequest)
        {
            bool isNotMatchedARoleClaimWithRequestRoles =
                GetRoles().FirstOrDefault(tokenRole => securedRequest.Roles.Any(RequestRole => RequestRole == tokenRole)).IsNullOrEmpty();
            if (isNotMatchedARoleClaimWithRequestRoles) throw new ForbiddenException();
        }
    }
}

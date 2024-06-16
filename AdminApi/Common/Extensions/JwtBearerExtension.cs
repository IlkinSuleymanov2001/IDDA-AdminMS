using Application.Services.Security;
using Domain.HelperClasses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AdminApi.Commons.Extensions
{
    public static class JwtBearerExtension
    {

        public static IServiceCollection AddSwaggerAndJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,

                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityService.CreateSecurityKey(tokenOptions.SecurityKey),
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                            expires != null ? expires > DateTime.UtcNow : false
                    };
                });

            return services;
        }
    }
}

﻿using Core.Exceptions;
using Core.Services.Security;
using MediatR;
using Microsoft.IdentityModel.Tokens;


namespace Core.Pipelines.Authorization
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest :IBaseRequest, ISecuredRequest
    {
        private readonly ISecurityService _securityService;

        public AuthorizationBehavior(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //List<string>? roleClaims = _httpContextAccessor.HttpContext.User.Claims(ClaimTypes.Role);
            IEnumerable<string> roleClaims  = _securityService.GetRoles();

            bool isNotMatchedARoleClaimWithRequestRoles =
                roleClaims.FirstOrDefault(roleClaim => request.Roles.Any(role => role == roleClaim)).IsNullOrEmpty();
            // user admin        
            if (isNotMatchedARoleClaimWithRequestRoles) throw new UnAuthorizationException("You are not authorized.");

            TResponse response = await next();
            return response;
        }
    }
}
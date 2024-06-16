﻿using Application.Common.Exceptions;
using Application.Services.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace Application.Common.Pipelines.Authorization
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISecurityService _securityService;

        public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor, ISecurityService securityService)
        {
            _httpContextAccessor = httpContextAccessor;
            _securityService = securityService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //List<string>? roleClaims = _httpContextAccessor.HttpContext.User.Claims(ClaimTypes.Role);
            IEnumerable<string> roleClaims  = _securityService.GetRoles(_httpContextAccessor);

            bool isNotMatchedARoleClaimWithRequestRoles =
                roleClaims.FirstOrDefault(roleClaim => request.Roles.Any(role => role == roleClaim)).IsNullOrEmpty();

            if (isNotMatchedARoleClaimWithRequestRoles) throw new UnAuthorizationException("You are not authorized.");

            TResponse response = await next();
            return response;
        }
    }
}

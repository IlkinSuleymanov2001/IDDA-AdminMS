using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Futures.Organization.Dtos;
using Application.Repositories;
using AutoMapper;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Response;
using Core.Services.Security;
using MediatR;

namespace Application.Futures.Organization.Queries.Get
{
    public record GetOrganizationQueryRequest(string OrganizationName) : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.USER];
    }

    public class GetOrganizationQueryHandler(
        IOrganizationRepository organizationRepository,
        ISecurityService securityService,
        IMapper mapper
        )
        : IRequestHandler<GetOrganizationQueryRequest, IDataResponse>
    {
        public async Task<IDataResponse> Handle(GetOrganizationQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Organization org;
            if (securityService.CurrentRoleEqualsTo(Role.ADMIN))
                org = await organizationRepository.GetAsync(c => c.Name == request.OrganizationName,filterIgnore: true);
            else  
                org = await organizationRepository.GetAsync(c => c.Name == request.OrganizationName);

            if(org == null) throw new NotFoundException(Messages.NotFoundOrganization);
            return new DataResponse
            {
                Data = mapper.Map<OrganizationDto>(org)
            };
        }
    }



}

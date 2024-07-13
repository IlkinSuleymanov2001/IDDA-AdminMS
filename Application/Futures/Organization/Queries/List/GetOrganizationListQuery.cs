using System.Linq.Expressions;
using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Futures.Organization.Dtos;
using Application.Repositories;
using AutoMapper;
using Core.Pipelines.Authorization;
using Core.Response;
using MediatR;

namespace Application.Futures.Organization.Queries.List
{
    public  record  GetOrganizationListQuery:ISecuredRequest, IQuery<IDataResponse>
    {
           public string[] Roles => [Role.USER];
    }

    public class GetOrganizationListQueryHandler(IOrganizationRepository organizationRepository, IMapper mapper)
        : IRequestHandler<GetOrganizationListQuery, IDataResponse>
    {
        public async Task<IDataResponse> Handle(GetOrganizationListQuery request, CancellationToken cancellationToken)
        {
            var orgList =  await organizationRepository.ListAsync(cancellationToken: cancellationToken);
            return DataResponse.Ok(mapper.Map<IEnumerable<OrganizationDto>>(orgList));

        }
    }
}

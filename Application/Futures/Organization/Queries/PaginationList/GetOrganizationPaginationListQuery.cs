using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Futures.Organization.Dtos;
using Application.Repositories;
using Application.Repositories.Cores.Paging;
using AutoMapper;
using Core.Pipelines.Authorization;
using Core.Response;
using MediatR;


namespace Application.Futures.Organization.Queries.GetList
{
    public record GetOrganizationPaginationListQuery(PageRequest PageRequest) : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN];
    }

    public class GetOrganizationListQueryHandler(IOrganizationRepository organizationRepository, IMapper mapper)
        : IRequestHandler<GetOrganizationPaginationListQuery, IDataResponse>
    {
        public async Task<IDataResponse> Handle(GetOrganizationPaginationListQuery request, CancellationToken cancellationToken)
        {
            var orgList =await  organizationRepository.
                GetListAsync(index: request.PageRequest.Page, 
                    size:request.PageRequest.PageSize, 
                    cancellationToken: cancellationToken,
                    filterIgnore:true);


            return DataResponse.Ok(mapper.Map<PaginateOrganizationModel>(orgList));

        }
    }
}

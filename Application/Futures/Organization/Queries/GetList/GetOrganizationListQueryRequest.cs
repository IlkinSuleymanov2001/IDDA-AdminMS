using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Repositories;
using Application.Repositories.Cores.Paging;
using Core.Pipelines.Authorization;
using Core.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Futures.Organization.Queries.GetList
{
    public record GetOrganizationListQueryRequest(PageRequest PageRequest) : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN];
    }

    public class GetOrganizationListQueryHandler : IRequestHandler<GetOrganizationListQueryRequest, IDataResponse>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public GetOrganizationListQueryHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<IDataResponse> Handle(GetOrganizationListQueryRequest request, CancellationToken cancellationToken)
        {
            var orgList =  await _organizationRepository.FindBy(size: request.PageRequest.PageSize,
                index: request.PageRequest.Page).AsNoTracking().Select(c=>new { c.Name }).ToListAsync();

            return new DataResponse() {Data=orgList,Message="success" };

        }
    }
}

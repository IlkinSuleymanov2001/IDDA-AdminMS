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
    public record GetOrganizationListQueryRequest(PageRequest PageRequest) : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN,Role.USER];
    }

    public class GetOrganizationListQueryHandler : IRequestHandler<GetOrganizationListQueryRequest, IDataResponse>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public GetOrganizationListQueryHandler(IOrganizationRepository organizationRepository, IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task<IDataResponse> Handle(GetOrganizationListQueryRequest request, CancellationToken cancellationToken)
        {
            var orgList =await  _organizationRepository.GetListAsync(index: request.PageRequest.Page, size:request.PageRequest.PageSize);

            return new DataResponse() {Data= _mapper.Map<PaginateOrganizationModel>(orgList) };

        }
    }
}

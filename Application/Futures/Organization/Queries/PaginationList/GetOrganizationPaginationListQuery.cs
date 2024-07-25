using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Futures.Organization.Dtos;
using Application.Repositories;
using Application.Repositories.Cores.Paging;
using AutoMapper;
using Core.Pipelines.Authorization;
using Core.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Futures.Organization.Queries.GetList
{
    public record GetOrganizationPaginationListQuery(PageRequest PageRequest) : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => new[] { Role.ADMIN };
    }

    public class GetOrganizationListQueryHandler : IRequestHandler<GetOrganizationPaginationListQuery, IDataResponse>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public GetOrganizationListQueryHandler(IOrganizationRepository organizationRepository, IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task<IDataResponse> Handle(GetOrganizationPaginationListQuery request, CancellationToken cancellationToken)
        {
            var orgList = await _organizationRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                filterIgnore: true,
                orderBy: q => q.OrderBy(o => !o.Active).ThenByDescending(o => o.CreatedAt), // Sıralama kriteri
                include: q => q.Include(o => o.Staffs) // İlgili tabloları dahil et
            );

            return DataResponse.Ok(_mapper.Map<PaginateOrganizationModel>(orgList));
        }
    }
}
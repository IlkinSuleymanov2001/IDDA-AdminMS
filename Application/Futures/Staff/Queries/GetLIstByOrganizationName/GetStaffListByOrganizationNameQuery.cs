using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Futures.Staff.Dtos;
using Application.Repositories;
using Application.Repositories.Cores.Paging;
using AutoMapper;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Futures.Staff.Queries.GetLIstByOrganizationName
{
    public record GetStaffListByOrganizationNameQuery(PageRequest PageRequest, string OrganizationName) : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN];

    }


    public class GetStaffListByOrganizationNameHandler(
        IStaffRepository staffRepository,
        IMapper mapper,
        IOrganizationRepository organization)
        : IRequestHandler<GetStaffListByOrganizationNameQuery, IDataResponse>
    {
        public async Task<IDataResponse> Handle(GetStaffListByOrganizationNameQuery request, CancellationToken cancellationToken)
        {
            var org = await organization.GetAsync(c => c.Name == request.OrganizationName,
                enableTracking: false, filterIgnore: true) ?? throw new NotFoundException(Messages.NotFoundOrganization);

            var staffList =
                await staffRepository.GetListAsync(predicate: c => c.Organization != null
                                                                   && c.Organization.Name == request.OrganizationName,
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    include: c => c.Include(c => c.Organization),
                    enableTracking: false,
                    filterIgnore: true,
                    cancellationToken: cancellationToken);

            return DataResponse.Ok(mapper.Map<PaginateStaffModel>(staffList));

        }
    }
}

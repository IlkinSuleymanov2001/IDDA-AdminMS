using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Futures.Staff.Dtos;
using Application.Repositories;
using Application.Repositories.Cores.Paging;
using AutoMapper;
using Core.Pipelines.Authorization;
using Core.Repository.Paging;
using Core.Response;
using Core.Services.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Futures.Staff.Queries.GetList
{
    public record GetStaffListQueryRequest(PageRequest PageRequest): IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN,Role.SUPER_STAFF,Role.STAFF];
    }

    public class GetStaffListQueryHandler(
        IStaffRepository staffRepository,
        ISecurityService securityService,
        IMapper mapper)
        : IRequestHandler<GetStaffListQueryRequest, IDataResponse>
    {
        public async Task<IDataResponse> Handle(GetStaffListQueryRequest request, CancellationToken cancellationToken)
        {
            IPaginate<Domain.Entities.Staff> staffList;

            if (securityService.CurrentRoleEqualsTo(Role.ADMIN))
                staffList = await staffRepository.GetListAsync(include: c => c.Include(c => c.Organization),
                    index: request.PageRequest.Page, size: request.PageRequest.PageSize, 
                    enableTracking: false,
                    filterIgnore:true,
                    cancellationToken: cancellationToken);
            else
            {
                var staff = await staffRepository.GetAsync(c => c.Username == securityService.GetUsername(),
                                                            include: c => c.Include(c => c.Organization),
                                                             enableTracking: false);

                staffList = await staffRepository.GetListAsync(c => c.OrganizationID == staff.OrganizationID,
                    include: c => c.Include(c => c.Organization),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    enableTracking: false,
                    cancellationToken: cancellationToken);
            }

            return DataResponse.Ok(mapper.Map<PaginateStaffModel>(staffList));

        }
    }


}

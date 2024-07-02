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

    public class GetStaffListQueryHandler : IRequestHandler<GetStaffListQueryRequest, IDataResponse>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;

        public GetStaffListQueryHandler(IStaffRepository staffRepository, ISecurityService securityService, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _securityService = securityService;
            _mapper = mapper;
        }

        public async Task<IDataResponse> Handle(GetStaffListQueryRequest request, CancellationToken cancellationToken)
        {
            IPaginate<Domain.Entities.Staff> staffList=default;

            if (_securityService.IsHaveRole(Role.ADMIN))
                staffList = await _staffRepository.GetListAsync(include: c => c.Include(c => c.Organization),
                    index: request.PageRequest.Page, size: request.PageRequest.PageSize, enableTracking: false);
            else
            {
                var staff = await _staffRepository.GetAsync(c => c.Username == _securityService.GetUsername()
                , include: c => c.Include(c => c.Organization), enableTracking: false);

                staffList = await _staffRepository.GetListAsync(c => c.OrganizationID == staff.OrganizationID,
                    include: c => c.Include(c => c.Organization), index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize, enableTracking: false);
            }

            return DataResponse.Ok(_mapper.Map<PaginateStaffModel>(staffList));

        }
    }


}

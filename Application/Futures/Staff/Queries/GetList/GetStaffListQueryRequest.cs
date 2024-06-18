using Application.Common.Pipelines.Authorization;
using Application.Common.Pipelines.Transaction;
using Application.Common.Response;
using Application.Futures.Staff.Dtos;
using Application.Repositories;
using Application.Repositories.Cores.Paging;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Futures.Staff.Queries.GetList
{
    public record GetStaffListQueryRequest(PageRequest PageRequest): IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => ["ADMIN","GOVERMENT"];
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
            var admin = _securityService.IsAdmin();
            IEnumerable<Domain.Entities.Staff> staffList;
            if (admin)
                staffList = await _staffRepository.GetListAsync(include: c => c.Include(c => c.Organization),
                    index:request.PageRequest.Page,size:request.PageRequest.PageSize,enableTracking:false);
            else 
            {
                var staff = await _staffRepository.GetAsync(c=>c.Username== _securityService.GetUsername()
                ,include:c=>c.Include(c=>c.Organization),enableTracking:false);


                staffList = await _staffRepository.GetListAsync(c => c.OrganizationID ==staff.OrganizationID,
                    include: c => c.Include(c => c.Organization),index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize, enableTracking: false);
            }

            return new DataResponse 
            {
                Data = _mapper.Map<IEnumerable<StaffListDto>>(staffList),
                Message ="success"
            };
               



        }
    }


}

using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Futures.Staff.Dtos;
using Application.Repositories;
using AutoMapper;
using Core.Pipelines.Authorization;
using Core.Response;
using Core.Services.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Futures.Staff.Queries.Get
{
    public record GetStaffQueryRequest : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN, Role.STAFF, Role.SUPER_STAFF];
    }

    public class GetStaffQueryHandler : IRequestHandler<GetStaffQueryRequest, IDataResponse>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;

        public GetStaffQueryHandler(IStaffRepository staffRepository, ISecurityService securityService, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _securityService = securityService;
            _mapper = mapper;
        }

        public async Task<IDataResponse> Handle(GetStaffQueryRequest request, CancellationToken cancellationToken)
        {
            var username = _securityService.GetUsername();
            var currentStaff = await _staffRepository.GetAsync(g => g.Username == username
            , include: ef => ef.Include(c => c.Organization));
            return new DataResponse
            {
                Data = _mapper.Map<StaffDto>(currentStaff)
            };
        }
    }
}

using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Repositories;
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

        public GetStaffQueryHandler(IStaffRepository staffRepository, ISecurityService securityService)
        {
            _staffRepository = staffRepository;
            _securityService = securityService;
        }

        public async Task<IDataResponse> Handle(GetStaffQueryRequest request, CancellationToken cancellationToken)
        {
            var username = _securityService.GetUsername();
            var currentStaff = await _staffRepository.GetAsync(g => g.Username == username
            , include: ef => ef.Include(c => c.Organization));
            return new DataResponse
            {
                Data = new
                {
                    currentStaff.Fullname,
                    currentStaff.Username,
                    OrganizationName = currentStaff.Organization.Name
                },
                Message = "success"
            };
        }
    }
}

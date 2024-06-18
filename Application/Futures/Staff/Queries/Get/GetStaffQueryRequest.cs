using Application.Common.Pipelines.Authorization;
using Application.Common.Pipelines.Transaction;
using Application.Common.Response;
using Application.Repositories;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Futures.Staff.Queries.Get
{
    public record GetStaffQueryRequest : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => ["GOVERMENT"];
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

using Application.Common.Pipelines.Transaction;
using Application.Repositories;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Futures.Staff.Queries.Search
{
    public record SearchStaffQueryRequest(string Username) : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => ["ADMIN","GOVERMENT"];
    }

    public class SearchStaffQueryHandler : IRequestHandler<SearchStaffQueryRequest, IDataResponse>
    {
        private readonly IStaffRepository _staffRepository;

        public SearchStaffQueryHandler(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<IDataResponse> Handle(SearchStaffQueryRequest request, CancellationToken cancellationToken)
        {
            var currentStaff = await _staffRepository.GetAsync(g => g.Username == request.Username
            ,include: ef => ef.Include(c => c.Organization));

            if (currentStaff is null) throw new NotFoundException("current not found");
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

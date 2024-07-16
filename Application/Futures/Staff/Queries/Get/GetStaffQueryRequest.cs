using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Futures.Staff.Dtos;
using Application.Repositories;
using AutoMapper;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Response;
using Core.Services.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Futures.Staff.Queries.Get
{
    public record GetStaffQueryRequest : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.STAFF];
    }

    public class GetStaffQueryHandler(
        IStaffRepository staffRepository,
        ISecurityService securityService,
        IMapper mapper)
        : IRequestHandler<GetStaffQueryRequest, IDataResponse>
    {
        public async Task<IDataResponse> Handle(GetStaffQueryRequest request, CancellationToken cancellationToken)
        {
            var username = securityService.GetUsername();
            var currentStaff = await staffRepository.GetAsync(g => g.Username == username
                                   , include: ef => ef.Include(c => c.Organization)!)
                               ?? throw new NotFoundException(Messages.NotFoundStaff);

            return new DataResponse
            {
                Data = mapper.Map<StaffDto>(currentStaff)
            };
        }
    }
}

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


namespace Application.Futures.Staff.Queries.Search
{
    public record SearchStaffQueryRequest(string Username) : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN, Role.STAFF, Role.SUPER_STAFF];
    }


    public class SearchStaffQueryHandler(IStaffRepository staffRepository, IMapper mapper,ISecurityService service)
        : IRequestHandler<SearchStaffQueryRequest, IDataResponse>
    {
        public async Task<IDataResponse> Handle(SearchStaffQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Staff currentStaff;
            if (service.CurrentRoleEqualsTo(Role.ADMIN))
            {
                 currentStaff = await staffRepository.GetAsync(g => g.Username == request.Username
                                       ,include: ef => ef.Include(c => c.Organization)!,
                                       filterIgnore: true)
                                       ?? throw new NotFoundException(Messages.NotFoundStaff);
            }
            else
            {
                 currentStaff = await staffRepository.GetAsync(g => g.Username == request.Username, include: ef => ef.Include(c => c.Organization)!)
                                   ?? throw new NotFoundException(Messages.NotFoundStaff);
            }

            return DataResponse.Ok(mapper.Map<StaffDto>(currentStaff));

        }
    }
}

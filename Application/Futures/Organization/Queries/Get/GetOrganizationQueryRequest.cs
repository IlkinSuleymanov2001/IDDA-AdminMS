using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Futures.Organization.Dtos;
using Application.Repositories;
using AutoMapper;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Response;
using Core.Services.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Futures.Organization.Queries.Get
{
    public record GetOrganizationQueryRequest(string OrganizationName) : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.USER];
    }

    public class GetOrganizationQueryHandler(
        IOrganizationRepository organizationRepository,
        ISecurityService securityService,
        IMapper mapper,
        IStaffRepository staffRepository)
        : IRequestHandler<GetOrganizationQueryRequest, IDataResponse>
    {
        public async Task<IDataResponse> Handle(GetOrganizationQueryRequest request, CancellationToken cancellationToken)
        {
            var org = await organizationRepository.GetAsync(c=>c.Name==request.OrganizationName) 
                      ?? throw new NotFoundException();


        /*    var staff = await _staffRepository.GetAsync(c=>c.Username == _securityService.GetUsername(),
                include:e=>e.Include(c=>c.Organization),enableTracking:false);
            if (staff.Organization.Name != org.Name) throw new ForbiddenException();*/

            return new DataResponse { Data = mapper.Map<OrganizationDto>(org) };
        }
    }



}

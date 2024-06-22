using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Futures.Organization.Dtos;
using Application.Repositories;
using AutoMapper;
using Core.Pipelines.Authorization;
using Core.Response;
using Core.Services.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Futures.Organization.Queries.Get
{
    public record GetOrganizationQueryRequest(string OrganizationName) : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => [nameof(Role.ADMIN),nameof(Role.STAFF),nameof(Role.USER)];
    }

    public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQueryRequest, IDataResponse>
    {

        private readonly IOrganizationRepository _organizationRepository;
        private readonly ISecurityService _securityService;
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetOrganizationQueryHandler(IOrganizationRepository organizationRepository, ISecurityService securityService, IMapper mapper, IStaffRepository staffRepository)
        {
            _organizationRepository = organizationRepository;
            _securityService = securityService;
            _mapper = mapper;
            _staffRepository = staffRepository;
        }

        public async Task<IDataResponse> Handle(GetOrganizationQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Organization org=default;

            if (_securityService.IsHaveRole(Role.ADMIN))
            {
                org = await _organizationRepository.GetAsync(c => c.Name == request.OrganizationName, include: c => c.Include(c => c.Staffs));
                return new DataResponse { Data = _mapper.Map<OrganizationDto>(org) };
            }
            else if (_securityService.IsHaveRole(Role.STAFF))
            {
                var username = _securityService.GetUsername();

                var staff = await _staffRepository.GetAsync(c => c.Username == username, include: c => c.Include(c => c.Organization));
                if (staff != null && staff.Organization.Name == request.OrganizationName)
                {
                    org = await _organizationRepository.GetAsync(c => c.Name == request.OrganizationName, include: c => c.Include(c => c.Staffs));
                    return new DataResponse { Data = _mapper.Map<OrganizationDto>(org) };
                }
            }
            
            org = await _organizationRepository.GetAsync(c => c.Name == request.OrganizationName);


            return new DataResponse { Data = new { org.Name } };
        }
    }



}

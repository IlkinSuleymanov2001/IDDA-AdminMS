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

namespace Application.Futures.Organization.Queries.Get
{
    public record GetOrganizationQueryRequest(string OrganizationName) : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN,Role.STAFF,Role.USER];
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

            var org = await _organizationRepository.GetAsync(c=>c.Name==request.OrganizationName);
            if (org is null) throw new NotFoundException(typeof(Domain.Entities.Organization));

            return new DataResponse { Data = _mapper.Map<OrganizationDto>(org) };
        }
    }



}

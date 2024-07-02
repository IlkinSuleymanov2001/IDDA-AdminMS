using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Futures.Staff.Dtos;
using Application.Repositories;
using Application.Repositories.Cores.Paging;
using AutoMapper;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Futures.Staff.Queries.GetLIstByOrganizationName
{
    public record GetStaffListByOrganizationNameQuery(PageRequest PageRequest, string OrganizationName) : IQuery<IDataResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN];
    }

    public class GetStaffListByOrganizationNameHandler : IRequestHandler<GetStaffListByOrganizationNameQuery, IDataResponse>
    {
        IOrganizationRepository organizationRepository;
        IStaffRepository staffRepository;
        IMapper mapper;

        public GetStaffListByOrganizationNameHandler(IStaffRepository staffRepository, IMapper mapper, IOrganizationRepository organization)
        {
            this.staffRepository = staffRepository;
            this.mapper = mapper;
            this.organizationRepository = organization;
        }

        public async Task<IDataResponse> Handle(GetStaffListByOrganizationNameQuery request, CancellationToken cancellationToken)
        {
            var org  = await organizationRepository.GetAsync(c => c.Name == request.OrganizationName);
            if (org is null) throw new NotFoundException();

            var staffList = await staffRepository.GetListAsync(predicate:c=>c.Organization.Name==request.OrganizationName,
                index:request.PageRequest.Page,size:request.PageRequest.PageSize,
                include: c => c.Include(c => c.Organization));

            return DataResponse.Ok(mapper.Map<PaginateStaffModel>(staffList));

        }
    }
}


using Application.Common.Exceptions;
using Application.Common.Pipelines.Authorization;
using Application.Common.Pipelines.Transaction;
using Application.Common.Response;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Futures.Staff.Commands.Create
{
    public record CreateStaffCommandRequest(string? Fullname, string Username, string OrganizationName) : ICommand<IResponse>,ISecuredRequest
    {
        public string[] Roles => ["ADMIN"];
    }

    public class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommandRequest, IResponse>
    {

        public readonly IStaffRepository StaffRepository;
        private readonly IOrganizationRepository OrganizationRepository;
        private readonly IMapper _mapper;



        public CreateStaffCommandHandler(IStaffRepository staffRepository,
            IOrganizationRepository organizationRepository,
            IMapper mapper)
        {
            StaffRepository = staffRepository;
            OrganizationRepository = organizationRepository;
            _mapper = mapper;
        }



        public async Task<IResponse> Handle(CreateStaffCommandRequest request, CancellationToken cancellationToken)
        {
           var organization =  await OrganizationRepository.GetAsync(c => c.Name == request.OrganizationName);
           if (organization is null) throw new NotFoundException("Organization not found");

            var staff = _mapper.Map<Domain.Entities.Staff>(request);
            staff.Organization = organization;
            await StaffRepository.CreateAsync(staff);

            return new Response{ Message = "Staff success created" };

        }
    }
}

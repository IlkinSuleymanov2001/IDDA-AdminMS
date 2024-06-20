using Application.Repositories;
using AutoMapper;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using MediatR;

namespace Application.Futures.Staff.Commands.Create
{
    public record CreateStaffCommandRequest(string? Fullname, string Username, string OrganizationName) :ICommandSavePoint<IResponse>,ISecuredRequest
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

using Application.Futures.Constants;
using Application.Repositories;
using AutoMapper;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using MediatR;

namespace Application.Futures.Staff.Commands.Create
{
    public record CreateStaffCommandRequest(string? Fullname, string Username, string OrganizationName) :ICommand<IResponse>,ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN,Role.SUPER_STAFF];
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
           if (organization is null) throw new NotFoundException(typeof(Domain.Entities.Organization));

            var isHaveStaff = await StaffRepository.AnyAsync(c => c.Username == request.Username);
            if (isHaveStaff) throw new DublicatedEntityException(typeof(Domain.Entities.Organization));
           
            var staff = _mapper.Map<Domain.Entities.Staff>(request);
            staff.OrganizationID = organization.Id;
            await StaffRepository.CreateAsync(staff);
            await StaffRepository.SaveChangesAsync();
            return new Response();

        }
    }
}

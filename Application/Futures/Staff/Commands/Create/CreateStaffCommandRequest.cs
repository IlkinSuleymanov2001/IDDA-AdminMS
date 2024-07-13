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

    public class CreateStaffCommandHandler(
        IStaffRepository staffRepository,
        IOrganizationRepository organizationRepository)
        : IRequestHandler<CreateStaffCommandRequest, IResponse>
    {



        public async Task<IResponse> Handle(CreateStaffCommandRequest request, CancellationToken cancellationToken)
        {
           var organization =  await organizationRepository.GetAsync(c => c.Name == request.OrganizationName)
               ?? throw new NotFoundException();

           var exsitsStaff = await staffRepository.AnyAsync(c => c.Username.Contains(request.Username));
            if (exsitsStaff)  throw new DublicatedEntityException();
           
            var staff = new Domain.Entities.Staff(request.Fullname, request.Username, organization.Id);
            await staffRepository.CreateAsync(staff);
            await staffRepository.SaveChangesAsync(cancellationToken);
            return  Response.Ok();

        }
    }
}

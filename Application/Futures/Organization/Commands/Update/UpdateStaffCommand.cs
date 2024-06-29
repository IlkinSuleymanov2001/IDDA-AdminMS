using Application.Futures.Constants;
using Application.Repositories;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using MediatR;

namespace Application.Futures.Organization.Commands.Update
{
    public record UpdateStaffCommand(string OldName, string NewName) :ICommand<IResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN,Role.SUPER_STAFF];
    }


    public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand, IResponse>
    {
        IOrganizationRepository _organizationRepository;

        public UpdateStaffCommandHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<IResponse> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
           var org =  await _organizationRepository.GetAsync(c => c.Name == request.OldName);
            if (org == null) throw new NotFoundException("organization  not found");
            org.Name = request.NewName;

            await _organizationRepository.UpdateAsync(org);
            await _organizationRepository.SaveAsync();

            return  Response.Ok();

        }
    }
}

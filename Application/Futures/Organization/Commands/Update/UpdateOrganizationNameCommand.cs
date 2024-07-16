using Application.Futures.Constants;
using Application.Repositories;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using MediatR;

namespace Application.Futures.Organization.Commands.Update
{
    public record UpdateOrganizationNameCommand(string OldName, string NewName) :ICommand<IResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN,Role.SUPER_STAFF];
    }


    public class UpdateStaffCommandHandler(IOrganizationRepository organizationRepository)
        : IRequestHandler<UpdateOrganizationNameCommand, IResponse>
    {
        public async Task<IResponse> Handle(UpdateOrganizationNameCommand request, CancellationToken cancellationToken)
        {
           var org =  await organizationRepository.GetAsync(c => c.Name == request.OldName,enableTracking:true)
               ?? throw new NotFoundException(Messages.NotFoundOrganization);
            org.Name = request.NewName;

            //await organizationRepository.UpdateAsync(org);
            await organizationRepository.SaveChangesAsync(cancellationToken);

            return  Response.Ok();

        }
    }
}

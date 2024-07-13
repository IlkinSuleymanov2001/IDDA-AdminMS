
using Application.Futures.Constants;
using Application.Repositories;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using MediatR;

namespace Application.Futures.Organization.Commands.Update
{
    public record UpdateOrganizationActiveOrDeActiveCommand(string Name):ICommand<IDataResponse>,ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN];

        public class UpdateStaffCommandHandler(IOrganizationRepository organizationRepository)
            : IRequestHandler<UpdateOrganizationActiveOrDeActiveCommand, IDataResponse>
        {
            public async Task<IDataResponse> Handle(UpdateOrganizationActiveOrDeActiveCommand request, CancellationToken cancellationToken)
            {
                var org = await organizationRepository.
                    GetAsync(c => c.Name == request.Name) 
                    ?? throw new NotFoundException("organization  not found");

                org.Active = org.Active switch
                {
                    true => false,
                    false => true,
                    _ => true
                };

                await organizationRepository.UpdateAsync(org);
                await organizationRepository.SaveChangesAsync(cancellationToken);
                //todo to be contiunue 
                return DataResponse.Ok(new ());

            }
        }
    }
}

using Application.Futures.Constants;
using Application.Repositories;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Application.Futures.Organization.Commands.Delete;

public record RemoveOrganizationCommandRequest([NotNull] string Name) : ICommand<IResponse>, ISecuredRequest
{
    public string[] Roles => [Role.ADMIN];

}
public class RemoveOrganizationCommandHandler(IOrganizationRepository organizationRepository)
    : IRequestHandler<RemoveOrganizationCommandRequest, IResponse>
{
    public async Task<IResponse> Handle(RemoveOrganizationCommandRequest request, CancellationToken cancellationToken)
    {
        var result =  organizationRepository.DeleteWhere(c => c.Name == request.Name,ignoreFilter:true);
        if (!result) throw new NotFoundException(Messages.NotFoundOrganization);
        await organizationRepository.SaveChangesAsync(cancellationToken);
        return  Response.Ok();

    }
}

using Application.Futures.Constants;
using Application.Repositories;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using Domain.Entities;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Application.Futures.Organization.Commands.Delete;

public record RemoveOrganizationCommandRequest([NotNull] string Name) : ICommand<IResponse>, ISecuredRequest
{
    public string[] Roles => [Role.ADMIN];

}
public class RemoveOrganiztionCommandHandler : IRequestHandler<RemoveOrganizationCommandRequest, IResponse>
{
    public RemoveOrganiztionCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    private readonly IOrganizationRepository _organizationRepository;

    public async Task<IResponse> Handle(RemoveOrganizationCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _organizationRepository.DeleteWhere(c => c.Name == request.Name);
        if (!result) throw new NotFoundException(typeof(Domain.Entities.Organization));
        return new Response();

    }
}

using Application.Futures.Constants;
using Application.Repositories;
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
public class RemoveOrganiztionCommandHandler : IRequestHandler<RemoveOrganizationCommandRequest, IResponse>
{
    public RemoveOrganiztionCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    private readonly IOrganizationRepository _organizationRepository;

    public async Task<IResponse> Handle(RemoveOrganizationCommandRequest request, CancellationToken cancellationToken)
    {
        await _organizationRepository.DeleteWhere(c => c.Name == request.Name);
        return new Response
        {
            Message = $"Deleted  success {request.Name} Organization"
        };

    }
}

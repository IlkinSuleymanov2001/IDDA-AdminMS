using Application.Repositories;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Application.Futures.Organization.Create;

public record CreateOrganizationCommandRequest([NotNull]string Name) : ICommand<IResponse>, ISecuredRequest
    {
        public string[] Roles => ["ADMIN"];
    }

public class CreateOrganiztionCommandHandler : IRequestHandler<CreateOrganizationCommandRequest, IResponse>
{
    public CreateOrganiztionCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    private readonly IOrganizationRepository _organizationRepository;

    public async Task<IResponse> Handle(CreateOrganizationCommandRequest request, CancellationToken cancellationToken)
    {
      await   _organizationRepository.CreateAsync(new Domain.Entities.Organization {Name = request.Name });
      return new Response 
      {
        Message =$"Created success {request.Name} Organization "
      };
         
    }
}

using Application.Futures.Constants;
using Application.Repositories;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using Domain.Entities;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Application.Futures.Organization.Commands.Create;

public record CreateOrganizationCommandRequest([NotNull] string Name) : ICommand<IResponse>,ISecuredRequest
{
    public string[] Roles => [Role.ADMIN];
}

public class CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository)
    : IRequestHandler<CreateOrganizationCommandRequest, IResponse>
{
    public async Task<IResponse> Handle(CreateOrganizationCommandRequest request, CancellationToken cancellationToken)
    {
        var isHaveOrganization  = await organizationRepository.AnyAsync(c=>c.Name==request.Name);
        if (isHaveOrganization) throw new DublicatedEntityException();
        await organizationRepository.CreateAsync(new Domain.Entities.Organization
        {
            Name = request.Name,
            Active = true
        });
        await organizationRepository.SaveChangesAsync(cancellationToken);

        return  Response.Ok();

    }
}

﻿using Application.Futures.Constants;
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

public class CreateOrganiztionCommandHandler : IRequestHandler<CreateOrganizationCommandRequest, IResponse>
{
    public CreateOrganiztionCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    private readonly IOrganizationRepository _organizationRepository;

    public async Task<IResponse> Handle(CreateOrganizationCommandRequest request, CancellationToken cancellationToken)
    {
        var isHaveOrganization  = await _organizationRepository.AnyAsync(c=>c.Name==request.Name);
        if (isHaveOrganization) throw new DublicatedEntityException(typeof(Domain.Entities.Organization));
        await _organizationRepository.CreateAsync(new Domain.Entities.Organization { Name = request.Name });
        await _organizationRepository.SaveAsync();

        return  Response.Ok();

    }
}

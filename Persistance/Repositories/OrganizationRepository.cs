using Application.Repositories;
using Application.Repositories.Context;
using Core.Repository;
using Domain.Entities;

namespace Infastructure.Repositories
{
    public class OrganizationRepository(AdminContext context)
        : EfRepository<AdminContext, Organization, int>(context), IOrganizationRepository;
}
    
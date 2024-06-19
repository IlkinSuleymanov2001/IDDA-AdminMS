using Application.Repositories;
using Application.Repositories.Context;
using Core.Repository;
using Domain.Entities;

namespace Infastructure.Repositories
{
    public class OrginazitionRepository : EFRepository<AdminContext, Organization, int>, IOrganizationRepository
    {
        public OrginazitionRepository(AdminContext context) : base(context)
        {
        }
    }
}

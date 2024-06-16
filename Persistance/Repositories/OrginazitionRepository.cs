using Application.Repositories;
using Domain.Entities;
using Infastructure.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Repositories
{
    public class OrginazitionRepository : EFRepositroy<Organization, int>, IOrganizationRepository
    {
        public OrginazitionRepository(AdminContext context) : base(context)
        {
        }
    }
}

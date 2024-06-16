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
    public class StaffRepository : EFRepositroy<Staff, int>,IStaffRepository
    {
        public StaffRepository(AdminContext context) : base(context)
        {
        }
    }
}

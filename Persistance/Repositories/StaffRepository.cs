using Application.Repositories;
using Application.Repositories.Context;
using Core.Repository;
using Domain.Entities;

namespace Infastructure.Repositories
{
    public class StaffRepository : EFRepository<AdminContext, Staff, int>,IStaffRepository
    {
        public StaffRepository(AdminContext context) : base(context)
        {
        }
    }
}

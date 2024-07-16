using Application.Repositories;
using Application.Repositories.Context;
using Core.Repository;
using Domain.Entities;

namespace Infastructure.Repositories
{
    public class StaffRepository(AdminContext context)
        : EfRepository<AdminContext, Staff, int>(context), IStaffRepository;
}

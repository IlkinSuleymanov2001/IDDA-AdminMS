using Application.Repositories;
using Application.Repositories.Context;
using Core.Repository;
using Domain.Entities;

namespace Infastructure.Repositories
{
    public class CategoryRepository : EFRepository<AdminContext, Category, int>, ICategoryRepository
    {
        public CategoryRepository(AdminContext context) : base(context)
        {
        }
    }
}

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
    public class CategoryRepository : EFRepositroy<Category, int>, ICategoryRepository
    {
        public CategoryRepository(AdminContext context) : base(context)
        {
        }
    }
}

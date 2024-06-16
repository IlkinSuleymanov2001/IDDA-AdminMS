using Application.Repositories;
using Application.Repositories.Cores;
using Infastructure.Repositories;
using Infastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infastructure
{
    public static  class InfastructureServices
    {
        public static IServiceCollection AddInfastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AdminContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ADMINMS"));
            });
 
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrganizationRepository, OrginazitionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
           

            return services;
        }
    }
}

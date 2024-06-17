using Application.Repositories;
using Application.Repositories.Context;
using Application.Repositories.Cores;
using Application.Services;
using Infastructure.Repositories;
using Infastructure.Repositories.Context;
using Infastructure.Services.Security;
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
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped(typeof(IRepository<,>), typeof(EFRepositroy<,>));


            return services;
        }
    }
}

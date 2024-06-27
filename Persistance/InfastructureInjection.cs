using Application.Repositories;
using Application.Repositories.Context;
using Infastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infastructure
{
    public static  class InfastructureInjection
    {
        public static IServiceCollection AddInfastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AdminContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("ADMINMS"));
            });

            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IOrganizationRepository, OrginazitionRepository>();

            return services;
        }
    }
}

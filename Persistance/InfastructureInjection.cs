using Application.Repositories;
using Application.Repositories.Context;
using Infastructure.Pipelines;
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
                options.UseSqlServer(configuration.GetConnectionString("ADMINMS"));
            });

            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrganizationRepository, OrginazitionRepository>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(_TransactionBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(_SavePointTransactionBehavior<,>));



            return services;
        }
    }
}

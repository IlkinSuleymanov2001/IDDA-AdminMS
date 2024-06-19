using Core.Pipelines.Logger;
using Core.Services.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class CoreInjection
    {
        public static IServiceCollection AddCoreService(this IServiceCollection services)
        {
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<Logger>();
            return services;
        }
    }
}

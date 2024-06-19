using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using Core;
using Core.Pipelines.Authorization;
using Core.Pipelines.Validation;
using Core.Pipelines.Logger;

namespace Application
{
    public static  class ApplicationServices
    {
        public static IServiceCollection AddApplicationsServices(this IServiceCollection services)
        {

            //services.AddFluentValidationAutoValidation();
            //services.AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));           
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddCoreService();







            return services;
        }
    }
}

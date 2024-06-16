using Application.Repositories.Cores;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using FluentValidation.AspNetCore;
using Application.Common.Pipelines.Validation;
using Application.Common.Pipelines.Authorization;
using Application.Services.Abstracts;
using Application.Services.Security;

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
            services.AddScoped<ISecurityService, SecurityService>();



            return services;
        }
    }
}

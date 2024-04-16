using Domain.DataServices;
using Domain.DataServices.Abstraction;
using Domain.Services;
using Domain.Services.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services
            .AddDataServices()
            .AddServices();
    }

    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        var assembly = typeof(IDataService)
            .Assembly;

        var serviceTypes = assembly
            .GetTypes()
            .Where(t => t.IsInterface)
            .Where(t => t.IsAssignableTo(typeof(IDataService)))
            .Where(t => t != typeof(IDataService))
            .ToList();

        foreach(var serviceType in serviceTypes)
        {
            var serviceImplementationType = assembly
                .GetTypes()
                .Where(t => t.IsClass && t.IsAssignableTo(serviceType))
                .First();

            services
                .AddScoped(serviceType, serviceImplementationType);
        }

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDomainConfigurationService, DomainConfigurationService>();
    }
}
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
        return services
            .AddScoped<IFamilyMemberService, FamilyMemberService>()
            .AddScoped<IFamilyService, FamilyService>();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDomainConfigurationService, DomainConfigurationService>();
    }
}
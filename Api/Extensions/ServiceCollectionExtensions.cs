using Api.HostedServices;
using Api.ModelBinderProviders;
using Data.Extensions;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();

        services
        .AddMvc(options =>
        {
            options.ModelBinderProviders.Insert(0, new ModelBinderProvider());
        });

        return services
            .AddSwagger()
            .AddHostedServices()
            .AddServices(configuration);
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
    }

    private static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        return services
            .AddHostedService<DomainConfigurationHostedService>()
            .AddHostedService<DbMigrationHostedService>();
    }

    private static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddDataServices(configuration)
            .AddDomainServices();
    }
}
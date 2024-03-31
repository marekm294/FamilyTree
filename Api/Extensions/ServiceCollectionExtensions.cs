using Api.HostedServices;
using Api.ModelBinderProviders;
using Data.Extensions;
using Domain.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;

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

        // Turn off default .Net Core validation.
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
            options.InvalidModelStateResponseFactory = (context) =>
            {
                var error = context!.ModelState.FirstOrDefault().Value!.Errors.FirstOrDefault()!.ErrorMessage;
                return new BadRequestObjectResult(error);
            };
        });

        //services.AddFluentValidationAutoValidation(config =>
        //{
            //config.DisableDataAnnotationsValidation = true;
        //});

        return services
            .AddSwagger()
            .AddHostedServices(configuration)
            .AddServices(configuration);
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
    }

    private static IServiceCollection AddHostedServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var sShouldMigrateDatabase = configuration.GetValue<bool>("ShouldMigrateDatabase");
        if (sShouldMigrateDatabase)
        {
            services
            .AddHostedService<DbMigrationHostedService>();
        }

        return services
            .AddHostedService<DomainConfigurationHostedService>();
    }

    private static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddDataServices(configuration)
            .AddDomainServices()
            .AddSharedServices();
    }
}
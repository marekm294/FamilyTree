﻿using Api.HostedServices;
using Api.ModelBinderProviders;
using Api.Services;
using Data.Extensions;
using Domain.Emails.Models;
using Domain.Extensions;
using Domain.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;
using Shared.JsonConverters;

namespace Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new PointJsonConverter());
            });

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
            .AddCors(configuration)
            .AddHostedServices(configuration)
            .AddOptionConfigurations(configuration)
            .AddServices(configuration);
    }

    private static IServiceCollection AddSwagger(
        this IServiceCollection services)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
    }

    public static IServiceCollection AddCors(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddCors(corsOptions =>
            {
                corsOptions.AddDefaultPolicy(corsPolicyBuilder =>
                {
                    var origins = configuration
                        .GetRequiredSection("Cors:Origins")
                        .Get<string[]>() ?? throw new NullReferenceException("No Cors origins string[] in config");

                    corsPolicyBuilder
                        .WithOrigins(origins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
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

    public static IServiceCollection AddOptionConfigurations(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .Configure<SmtpOptions>(configuration.GetSection(SmtpOptions.SEECTION_MANE));
    }

    private static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddApiServices()
            .AddDataServices(configuration)
            .AddDomainServices()
            .AddSharedServices();
    }

    private static IServiceCollection AddApiServices(
        this IServiceCollection services)
    {
        return services
            .AddScoped<ICurrentTenant, CurrentTenant>();
    }
}
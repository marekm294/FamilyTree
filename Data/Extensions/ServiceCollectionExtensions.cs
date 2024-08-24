using Data.DomainServices;
using Data.Queries;
using Data.Schemes.Abstraction;
using Data.Services;
using Data.Services.Abstraction;
using Domain.DataServicesAbstraction;
using Domain.Entities.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddDatabaseContext(configuration)
            .AddServices()
            .AddDomainServices()
            .AddQueries()
            .AddDbEntityFactories();
    }

    private static IServiceCollection AddDatabaseContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("Database") ?? throw new NotImplementedException("No connection string in configuration");

        return services.AddDbContext<AppDatabaseContext>(options =>
            options
                .UseSqlServer(
                    connectionString,
                    options =>
                    {
                        options.MigrationsHistoryTable("__MigrationsHistory");
                        options.UseNetTopologySuite();
                    })
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IMigrationService, MigrationService>();
    }

    private static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IQueryExecutor, QueryExecutor>()
            .AddSingleton<IEntityProvider, EntityProvider>()
            .AddScoped<IDbOperation, DbOperation>();
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        var entityTypes = GetIEntityTypes();

        foreach (var entityType in entityTypes)
        {
            var schemeType = GetSchemaForEntity(entityType);

            services.AddScoped(
                typeof(IQueryable<>).MakeGenericType(entityType),
                typeof(DbContextQuery<,>).MakeGenericType(schemeType, entityType));
        }

        return services;
    }

    private static IServiceCollection AddDbEntityFactories(this IServiceCollection services)
    {
        var entityTypes = GetIEntityTypes();

        foreach (var entityType in entityTypes)
        {
            var schemeType = GetSchemaForEntity(entityType);

            services.AddSingleton(
                typeof(IDbSchemeFactory<>).MakeGenericType(entityType),
                typeof(DbSchemeFactory<,>).MakeGenericType(schemeType, entityType));
        }

        return services;
    }

    private static List<Type> GetIEntityTypes()
    {
        var iEntityType = typeof(IEntity);

        return iEntityType
            .Assembly
            .GetTypes()
            .Where(t => t.IsInterface)
            .Where(t => t.IsAssignableTo(iEntityType))
            .Where(t => t != iEntityType)
            .ToList();
    }

    private static Type GetSchemaForEntity(Type entityType)
    {
        return typeof(DbScheme)
            .Assembly
            .GetTypes()
            .Where(t => t.IsClass)
            .First(t => t.IsAssignableTo(entityType));
    }
}
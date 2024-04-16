using Data.DomainServices;
using Data.Queries;
using Data.Schemes;
using Data.Services;
using Data.Services.Abstraction;
using Domain.DataServicesAbstraction;
using Domain.Entities;
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
                    x => x.MigrationsHistoryTable("__MigrationsHistory"))
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
        // TODO: replace by reflection
        return services
            .AddScoped<IQueryable<IFamilyMember>, DbContextQuery<FamilyMemberScheme, IFamilyMember>>()
            .AddScoped<IQueryable<IFamily>, DbContextQuery<FamilyScheme, IFamily>>();
    }

    private static IServiceCollection AddDbEntityFactories(this IServiceCollection services)
    {
        // TODO: replace by reflection
        return services
            .AddSingleton<IDbSchemeFactory<IFamilyMember>, DbSchemeFactory<FamilyMemberScheme, IFamilyMember>>()
            .AddSingleton<IDbSchemeFactory<IFamily>, DbSchemeFactory<FamilyScheme, IFamily>>();
    }
}
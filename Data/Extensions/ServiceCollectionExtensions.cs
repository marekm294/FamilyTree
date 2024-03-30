using Data.Entities;
using Data.Queries;
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
            .AddQueries();
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
                    x => x.MigrationsHistoryTable("__MigrationsHistory")));
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IMigrationService, MigrationService>();
    }

    private static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IQueryExecutor, QueryExecutor>();
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        return services
            .AddScoped<IQueryable<IFamilyMember>, DbContextQuery<FamilyMemberEntity, IFamilyMember>>();
    }
}
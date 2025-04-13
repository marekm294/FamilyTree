using Data;
using Data.Schemes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SystemTestsCore.Helpers;

namespace SystemTestsCore;

public class BaseDatabaseFixture<TFactory, TProgram> : IDisposable
    where TFactory : WebApplicationFactory<TProgram>, new()
    where TProgram : class
{
    public BaseDatabaseFixture()
    {
        Factory = new TFactory();
        var appDatabaseContext = Factory.Services.GetRequiredService<AppDatabaseContext>();

        var databaseFacade = appDatabaseContext.Database;
        databaseFacade.EnsureDeleted();
        databaseFacade.EnsureCreated();

        SeedSharedEntitiesAsync(appDatabaseContext).Wait();
    }

    public TFactory Factory { get; }

    public virtual void Dispose()
    {
    }

    private async Task SeedSharedEntitiesAsync(AppDatabaseContext appDatabaseContext)
    {
        var tenant1 = new TenantScheme()
        {
            Id = Constants.TenantId1,
            IsActive = true,
        };

        var tenant2 = new TenantScheme()
        {
            Id = Constants.TenantId2,
            IsActive = true,
        };

        await appDatabaseContext.AddAsync(tenant1);
        await appDatabaseContext.AddAsync(tenant2);
        await appDatabaseContext.SaveChangesAsync();
    }
}

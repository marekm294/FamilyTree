using Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

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
    }

    public TFactory Factory { get; }

    public virtual void Dispose()
    {
    }
}

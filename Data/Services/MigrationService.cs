using Data.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

internal sealed class MigrationService : IMigrationService
{
    private readonly AppDatabaseContext _appDatabaseContext;

    public MigrationService(AppDatabaseContext appDatabaseContext)
    {
        _appDatabaseContext = appDatabaseContext ?? throw new ArgumentNullException(nameof(appDatabaseContext));
    }

    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        await _appDatabaseContext.Database.MigrateAsync(cancellationToken);
    }
}
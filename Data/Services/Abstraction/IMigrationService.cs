namespace Data.Services.Abstraction;

public interface IMigrationService
{
    Task MigrateAsync(CancellationToken cancellationToken = default);
}
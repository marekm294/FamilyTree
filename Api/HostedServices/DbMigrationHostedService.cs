using Data.Services.Abstraction;

namespace Api.HostedServices;

public sealed class DbMigrationHostedService : IHostedService
{
    private readonly IServiceProvider _provider;

    public DbMigrationHostedService(IServiceProvider provider)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _provider.CreateAsyncScope();
        var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationService>();
        await migrationService.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
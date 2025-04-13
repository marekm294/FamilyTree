using Domain.Services.Abstraction;

namespace Api.HostedServices;

public sealed class DomainConfigurationHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DomainConfigurationHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var domainConfigurationService = _serviceProvider.GetRequiredService<IDomainConfigurationService>();
        domainConfigurationService.ConfigureDomain();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

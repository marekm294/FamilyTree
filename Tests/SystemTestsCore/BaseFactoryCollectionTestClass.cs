using Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SystemTestsCore.Helpers;

namespace SystemTestsCore;

public class BaseFactoryCollectionTestClass<TDatabaseFixture, TFactory, TProgram>
    where TDatabaseFixture : BaseDatabaseFixture<TFactory, TProgram>
    where TFactory : WebApplicationFactory<TProgram>, new()
    where TProgram : class
{
    protected readonly TDatabaseFixture _fixture;
    protected readonly TFactory _testWebApplicationFactory;
    protected readonly HttpClient _httpClient;
    protected IServiceScope _serviceScope = null!;
    internal AppDatabaseContext _appDatabaseContext = null!;

    public BaseFactoryCollectionTestClass(TDatabaseFixture fixture)
    {
        _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        _testWebApplicationFactory = fixture.Factory;
        _httpClient = _testWebApplicationFactory.CreateClient();

        // TODO: remove when TenantId is parsed from token.
        _httpClient.DefaultRequestHeaders.Add("Tenant", Constants.TenantId1.ToString());
    }

    protected Task InitializeDatabaseAsync()
    {
        _serviceScope = _testWebApplicationFactory.Services.CreateScope();
        _appDatabaseContext = _serviceScope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
        return Task.CompletedTask;
    }

    protected async Task ClearDatabaseAsync()
    {
        _serviceScope.Dispose();

        var serviceScope = _testWebApplicationFactory.Services.CreateScope();
        var appDatabaseContext = serviceScope.ServiceProvider.GetRequiredService<AppDatabaseContext>();

        appDatabaseContext.RemoveRange(appDatabaseContext.Weddings.IgnoreQueryFilters());
        appDatabaseContext.RemoveRange(appDatabaseContext.FamilyMembers.IgnoreQueryFilters());
        appDatabaseContext.RemoveRange(appDatabaseContext.Families.IgnoreQueryFilters());

        await appDatabaseContext.SaveChangesAsync();
    }
}
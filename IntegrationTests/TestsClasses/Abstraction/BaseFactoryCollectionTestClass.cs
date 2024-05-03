using Data;
using IntegrationTests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace IntegrationTests.TestsClasses.Abstraction;

[Collection(Constants.FACTORY_COLLECTION_FIXTURE)]
public class BaseFactoryCollectionTestClass : IAsyncLifetime
{
    protected readonly DatabaseFixture _fixture;
    protected readonly ITestOutputHelper _testOutputHelper;
    protected readonly TestWebApplicationFactory _testWebApplicationFactory;
    protected readonly HttpClient _httpClient;
    internal IServiceScope _serviceScope = null!;
    internal AppDatabaseContext _appDatabaseContext = null!;

    public BaseFactoryCollectionTestClass(
        DatabaseFixture fixture,
        ITestOutputHelper testOutputHelper)
    {
        _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        _testWebApplicationFactory = fixture.Factory;
        _httpClient = _testWebApplicationFactory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await ClearDatabaseAsync();
    }

    public Task InitializeAsync()
    {
        _serviceScope = _testWebApplicationFactory.Services.CreateScope();
        _appDatabaseContext = _serviceScope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
        return Task.CompletedTask;
    }

    private async Task ClearDatabaseAsync()
    {
        _serviceScope.Dispose();
        
        var serviceScope = _testWebApplicationFactory.Services.CreateScope();
        var appDatabaseContext = serviceScope.ServiceProvider.GetRequiredService<AppDatabaseContext>();

        appDatabaseContext.RemoveRange(appDatabaseContext.FamilyMembers);
        appDatabaseContext.RemoveRange(appDatabaseContext.Families);

        await appDatabaseContext.SaveChangesAsync();
    }
}
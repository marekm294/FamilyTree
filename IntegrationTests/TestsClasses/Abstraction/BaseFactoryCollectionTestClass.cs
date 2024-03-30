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
    internal readonly AppDatabaseContext _appDatabaseContext;

    public BaseFactoryCollectionTestClass(
        DatabaseFixture fixture,
        ITestOutputHelper testOutputHelper)
    {
        _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        _testWebApplicationFactory = fixture.Factory;
        _httpClient = _testWebApplicationFactory.CreateClient();
        _appDatabaseContext = _testWebApplicationFactory.Services.GetRequiredService<AppDatabaseContext>();
    }

    public async Task DisposeAsync()
    {
        await _appDatabaseContext.Database.EnsureDeletedAsync();
    }

    public async Task InitializeAsync()
    {
        await _appDatabaseContext.Database.EnsureCreatedAsync();
    }
}
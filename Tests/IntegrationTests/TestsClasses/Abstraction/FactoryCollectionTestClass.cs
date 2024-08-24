using Api;
using SystemTestsCore;
using SystemTestsCore.Helpers;
using Xunit.Abstractions;

namespace IntegrationTests.TestsClasses.Abstraction;

[Collection(Constants.FACTORY_COLLECTION_FIXTURE_INTEGRATION)]
public class FactoryCollectionTestClass
    : BaseFactoryCollectionTestClass<DatabaseFixture, TestWebApplicationFactory, Program>, IAsyncLifetime
{
    private readonly ITestOutputHelper _testOutputHelper;

    public FactoryCollectionTestClass(
        DatabaseFixture fixture,
        ITestOutputHelper testOutputHelper)
        : base(fixture)
    {
        _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
    }

    public async Task DisposeAsync()
    {
        await ClearDatabaseAsync();
    }

    public Task InitializeAsync()
    {
        return InitializeDatabaseAsync();
    }
}
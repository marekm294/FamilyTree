using IntegrationTests.Helpers;

namespace IntegrationTests;

public class DatabaseFixture : IDisposable
{
    public DatabaseFixture()
    {
        Factory = new TestWebApplicationFactory();
    }

    public TestWebApplicationFactory Factory { get; }

    public void Dispose()
    {
    }
}

[CollectionDefinition(Constants.FACTORY_COLLECTION_FIXTURE)]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
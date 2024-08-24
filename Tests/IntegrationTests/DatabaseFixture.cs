﻿using Api;
using SystemTestsCore;
using SystemTestsCore.Helpers;

namespace IntegrationTests;

public class DatabaseFixture : BaseDatabaseFixture<TestWebApplicationFactory, Program>
{
}

[CollectionDefinition(Constants.FACTORY_COLLECTION_FIXTURE_INTEGRATION)]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

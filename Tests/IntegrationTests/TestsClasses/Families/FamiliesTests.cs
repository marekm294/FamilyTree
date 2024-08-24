using IntegrationTests.TestsClasses.Abstraction;
using Xunit.Abstractions;

namespace IntegrationTests.TestsClasses.Families;

public partial class FamiliesTests : FactoryCollectionTestClass
{
    private const string FAMILIES_API = "/api/family";

    public FamiliesTests(
        DatabaseFixture fixture,
        ITestOutputHelper testOutputHelper)
        : base(fixture, testOutputHelper)
    {
    }
}
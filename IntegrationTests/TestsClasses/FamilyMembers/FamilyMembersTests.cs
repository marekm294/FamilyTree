using IntegrationTests.TestsClasses.Abstraction;
using Xunit.Abstractions;

namespace IntegrationTests.TestsClasses.FamilyMembers;

public partial class FamilyMembersTests : BaseFactoryCollectionTestClass
{
    private const string FAMILY_MEMBERS_API = "/api/familyMember";

    public FamilyMembersTests(
        DatabaseFixture fixture,
        ITestOutputHelper testOutputHelper)
        : base(fixture, testOutputHelper)
    {
    }
}
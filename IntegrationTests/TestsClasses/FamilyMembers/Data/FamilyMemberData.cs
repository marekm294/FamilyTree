using Data.Schemes;

namespace IntegrationTests.TestsClasses.FamilyMembers.Data;

internal static class FamilyMemberData
{
    public static FamilyMemberScheme GetFamilyMemberScheme(
        int i = 0)
    {
        return new FamilyMemberScheme()
        {
            FirstName = $"FirstName{i}",
            LastName = $"LastName{i}",
            BirthDate = DateTime.Now,
            DeathDate = DateTime.Now.AddDays(5),
        };
    }
}
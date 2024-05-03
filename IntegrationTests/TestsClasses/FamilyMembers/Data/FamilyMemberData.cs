using Data.Schemes;
using Shared.Types;

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
            Birth = new Event()
            {
                Date = new DateOnly(1997, 4, 29),
                Place = "Opava",
            },
            DeathDate = DateTime.Now.AddDays(5),
        };
    }
}
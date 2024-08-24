using Data.Schemes;

namespace IntegrationTests.TestsClasses.Families.Data;

internal static class FamilyData
{
    public static FamilyScheme GetFamilyScheme(int i = 0)
    {
        return new FamilyScheme()
        {
            FamilyName = $"FamilyName{i}"
        };
    }
}
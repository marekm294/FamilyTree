using Data.Schemes;
using SystemTestsCore.Helpers;

namespace IntegrationTests.TestsClasses.Families.Data;

internal static class FamilyData
{
    public static FamilyScheme GetFamilyScheme(
        int i = 0,
        bool shouldAddDefailtTenantId = true)
    {
        return new FamilyScheme()
        {
            FamilyName = $"FamilyName{i}",
            TenantId = shouldAddDefailtTenantId ? Constants.TenantId1 : default,
        };
    }
}
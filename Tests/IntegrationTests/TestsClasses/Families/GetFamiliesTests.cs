using Data.Schemes;
using IntegrationTests.TestsClasses.Families.Data;
using Shared.Helpers;
using Shared.Models.Outputs;
using System.Net;
using System.Text.Json;
using SystemTestsCore.Helpers;

namespace IntegrationTests.TestsClasses.Families;

public partial class FamiliesTests
{
    [Theory]
    [InlineData(3)]
    [InlineData(21)]
    public async Task GetFamilies_ShouldReturnAllFamilies_Async(int familyCount)
    {
        //Arrange
        for (var i = 0; i < familyCount; i++)
        {
            var familyScheme = FamilyData.GetFamilyScheme(i);
            await _appDatabaseContext.AddAsync(familyScheme);
        }

        await _appDatabaseContext.SaveChangesAsync();

        //Act
        var response = await _httpClient.GetAsync(FAMILIES_API);
        response.EnsureSuccessStatusCode();
        using var content = await response.Content.ReadAsStreamAsync();
        var familyOutputs = JsonSerializer.Deserialize<List<FamilyOutput>>(
            content,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(familyCount, familyOutputs!.Count);
        Assert.Equal(familyCount, familyOutputs!.Select(f => f.FamilyName).Distinct().Count());
    }

    [Fact]
    public async Task GetFamilies_ShouldReturnOnlyFamiliesWithCorrectTenantId_Async()
    {
        var familyCount = 3;
        for (var i = 0; i < familyCount; i++)
        {
            var familyScheme = FamilyData.GetFamilyScheme(i);
            await _appDatabaseContext.AddAsync(familyScheme);
        }

        var familySchemeWithDifferentTenant = FamilyData.GetFamilyScheme(familyCount + 1, false);
        familySchemeWithDifferentTenant.TenantId = Constants.TenantId2;
        await _appDatabaseContext.AddAsync(familySchemeWithDifferentTenant);

        await _appDatabaseContext.SaveChangesAsync();

        //Act
        var response = await _httpClient.GetAsync(FAMILIES_API);
        response.EnsureSuccessStatusCode();
        using var content = await response.Content.ReadAsStreamAsync();
        var familyOutputs = JsonSerializer.Deserialize<List<FamilyOutput>>(
            content,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(familyCount, familyOutputs!.Count);
        Assert.Equal(familyCount, familyOutputs!.Select(f => f.FamilyName).Distinct().Count());
    }
}
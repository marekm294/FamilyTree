using IntegrationTests.TestsClasses.Families.Data;
using Shared.Helpers;
using Shared.Models.Outputs;
using System.Net;
using System.Text.Json;

namespace IntegrationTests.TestsClasses.Families;

public partial class FamiliesTests
{
    [Theory]
    [InlineData(3)]
    [InlineData(21)]
    public async Task Get_Families_Success_Async(int familyCount)
    {
        //Arrange
        for(var i = 0; i < familyCount; i++)
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
}
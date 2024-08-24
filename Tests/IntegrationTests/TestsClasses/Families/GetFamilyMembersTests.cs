using IntegrationTests.TestsClasses.Families.Data;
using IntegrationTests.TestsClasses.FamilyMembers.Data;
using Shared.Helpers;
using Shared.Models.Outputs;
using System.Net;
using System.Text.Json;

namespace IntegrationTests.TestsClasses.Families;

public partial class FamiliesTests
{
    [Fact]
    public async Task Get_Family_Members_Success_Async()
    {
        //Arrange
        var familyScheme = FamilyData.GetFamilyScheme();
        await _appDatabaseContext.AddAsync(familyScheme);

        var familyScheme2 = FamilyData.GetFamilyScheme();
        await _appDatabaseContext.AddAsync(familyScheme2);

        var familyMembersCount = 10;
        var family1FamilyMembersCount = 10 / 2;

        var familyMembers = Enumerable
            .Range(0, familyMembersCount)
            .Select(FamilyMemberData.GetFamilyMemberScheme)
            .Select((fm, n) =>
            {
                fm.FamilyId = n < family1FamilyMembersCount ? familyScheme.Id : familyScheme2.Id;
                return fm;
            })
            .ToList();

        await _appDatabaseContext.AddRangeAsync(familyMembers);

        await _appDatabaseContext.SaveChangesAsync();

        //Act
        var response = await _httpClient.GetAsync($"{FAMILIES_API}/familyMembers/{familyScheme.Id}");
        response.EnsureSuccessStatusCode();
        using var content = await response.Content.ReadAsStreamAsync();
        var familyMemberOutputs = JsonSerializer.Deserialize<List<FamilyMemberOutput>>(
            content,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(family1FamilyMembersCount, familyMemberOutputs!.Count);
        Assert.Equal(familyScheme.Id, familyMemberOutputs.First()!.FamilyId);
    }

    [Fact]
    public async Task Get_Family_Members_Fail_Async()
    {
        //Arrange
        //Act
        var response = await _httpClient.GetAsync($"{FAMILIES_API}/familyMembers/{Guid.Empty}");

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
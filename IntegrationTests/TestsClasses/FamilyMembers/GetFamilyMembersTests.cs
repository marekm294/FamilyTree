using IntegrationTests.TestsClasses.Families.Data;
using IntegrationTests.TestsClasses.FamilyMembers.Data;
using Shared.Helpers;
using Shared.Models.Outputs;
using System.Net;
using System.Text.Json;

namespace IntegrationTests.TestsClasses.FamilyMembers;

public partial class FamilyMembersTests
{
    [Fact]
    public async Task Get_All_Family_Members_Success_Async()
    {
        //Arrange
        var families = Enumerable
            .Range(0, 5)
            .Select(FamilyData.GetFamilyScheme)
            .ToList();
        await _appDatabaseContext.AddRangeAsync(families);


        var familyMembers = Enumerable
            .Range(0, families.Count)
            .Select(FamilyMemberData.GetFamilyMemberScheme)
            .ToList();
        
        for(var i = 0; i < families.Count; i++)
        {
            familyMembers[i].FamilyId = families[i].Id;
        }

        await _appDatabaseContext.AddRangeAsync(familyMembers);
        await _appDatabaseContext.SaveChangesAsync();

        //Act
        var response = await _httpClient.GetAsync(FAMILY_MEMBERS_API);
        response.EnsureSuccessStatusCode();
        using var content = await response.Content.ReadAsStreamAsync();
        var familyMembersOutputs = JsonSerializer.Deserialize<List<FamilyMemberOutput>>(
            content,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(familyMembers.Count, familyMembersOutputs!.Count);
    }
}
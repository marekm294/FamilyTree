using Data.Schemes;
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
        var familyMembers = Enumerable
            .Range(0, 5)
            .Select(n => new FamilyMemberScheme()
            {
                FirstName = $"FirstName{n}",
                LastName = $"LastName{n}",
                BirthDate = DateTime.Now,
                DeathDate = DateTime.Now.AddDays(5),
            })
            .ToList();

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
        Assert.Equal(familyMembersOutputs!.Count, familyMembers.Count);
    }
}
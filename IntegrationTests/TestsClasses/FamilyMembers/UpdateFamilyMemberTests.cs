using Data.Schemes;
using Shared.Helpers;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Models.Outputs;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;

namespace IntegrationTests.TestsClasses.FamilyMembers;

public partial class FamilyMembersTests
{
    [Fact]
    public async Task Update_Family_Member_Success_Async()
    {
        //Arrange
        var familyMemberScheme = new FamilyMemberScheme()
        {
            FirstName = $"FirstName",
            LastName = $"LastName",
            BirthDate = DateTime.Now,
            DeathDate = DateTime.Now.AddDays(5),
        };

        await _appDatabaseContext.AddAsync(familyMemberScheme);
        await _appDatabaseContext.SaveChangesAsync();

        var updateFamilyMemberInput = new UpdateFamilyMemberInput()
        {
            Id = familyMemberScheme.Id,
            Version = familyMemberScheme.Version,
            FirstName = "Marek",
            LastName = "Mička",
            BirthDate = new DateTime(1997, 4, 29),
            DeathDate = null
        };

        //Act
        var response = await _httpClient.PutAsJsonAsync(
            FAMILY_MEMBERS_API,
            updateFamilyMemberInput,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        response.EnsureSuccessStatusCode();
        using var content = await response.Content.ReadAsStreamAsync();
        var familyMembersOutput = JsonSerializer.Deserialize<FamilyMemberOutput>(
            content,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEqual(Guid.Empty, familyMembersOutput!.Id);
        Assert.Equal(updateFamilyMemberInput.FirstName, familyMembersOutput!.FirstName);
        Assert.Equal(updateFamilyMemberInput.LastName, familyMembersOutput!.LastName);
    }
}
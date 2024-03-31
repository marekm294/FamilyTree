using Data.Schemes;
using Shared.Helpers;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Models.Outputs;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using IntegrationTests.TestsClasses.FamilyMembers.Data;
using Shared.QueryArgs;

namespace IntegrationTests.TestsClasses.FamilyMembers;

public partial class FamilyMembersTests
{
    [Fact]
    public async Task Update_Family_Member_Success_Async()
    {
        //Arrange
        var familyMemberScheme = FamilyMemberData.GetFamilyMemberScheme();

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

    [Theory]
    [InlineData(false, true, 0, HttpStatusCode.BadRequest)]
    [InlineData(true, true, 4, HttpStatusCode.BadRequest)]
        public async Task Update_Family_Member_Fail_Async(
        bool shouldSendInput,
        bool isErrorOutputExpected,
        int errorCount,
        HttpStatusCode expectedHttpStatusCode)
    {
        //Arrange
        var familyMemberScheme = FamilyMemberData.GetFamilyMemberScheme();

        await _appDatabaseContext.AddAsync(familyMemberScheme);
        await _appDatabaseContext.SaveChangesAsync();

        var updateFamilyMemberInput = new UpdateFamilyMemberInput()
        {
            Id = Guid.Empty,
            Version = [],
            FirstName = "",
            LastName = "",
            BirthDate = new DateTime(1997, 4, 29),
            DeathDate = null,
        };

        if (shouldSendInput is false)
        {
            updateFamilyMemberInput = null;
        }

        //Act
        var response = await _httpClient.PutAsJsonAsync(
            FAMILY_MEMBERS_API,
            updateFamilyMemberInput,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        //Assert
        Assert.Equal(expectedHttpStatusCode, response.StatusCode);

        if (isErrorOutputExpected)
        {
            using var content = await response.Content.ReadAsStreamAsync();
            var errorOutput = JsonSerializer.Deserialize<ErrorOutput>(
                content,
                JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

            Assert.Equal(errorCount, errorOutput?.ValidationErrors?.Count ?? 0);
        }
    }
}
using Shared.Helpers;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Models.Outputs;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using IntegrationTests.TestsClasses.FamilyMembers.Data;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.TestsClasses.FamilyMembers;

public partial class FamilyMembersTests
{
    [Fact]
    public async Task Update_Family_Member_Success_Async()
    {
        //Arrange
        var familyMemberScheme = FamilyMemberData.GetFamilyMemberScheme();
        familyMemberScheme.MiddleNames = ["Marek", "Honza"];

        await _appDatabaseContext.AddAsync(familyMemberScheme);
        await _appDatabaseContext.SaveChangesAsync();

        var updateFamilyMemberInput = new UpdateFamilyMemberInput()
        {
            Id = familyMemberScheme.Id,
            Version = familyMemberScheme.Version,
            FirstName = "Marek",
            LastName = "Mička",
            MiddleNames = ["Marek", "Honza", "Pavel"],
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
        Assert.Equal(updateFamilyMemberInput.MiddleNames.Length, familyMembersOutput!.MiddleNames.Length);
    }

    [Theory]
    [InlineData(true, true, false, true, true, 0, HttpStatusCode.BadRequest)]
    [InlineData(true, true, true, false, true, 2, HttpStatusCode.BadRequest)]
    [InlineData(true, false, true, true, true, 0, HttpStatusCode.NotFound)]
    [InlineData(false, true, true, true, true, 0, HttpStatusCode.NotFound)]
    public async Task Update_Family_Member_Fail_Async(
        bool shouldSendValidId,
        bool shouldSendValidVersion,
        bool shouldSendInput,
        bool shouldSendValidInput,
        bool isErrorOutputExpected,
        int errorCount,
        HttpStatusCode expectedHttpStatusCode)
    {
        //Arrange
        var familyMemberScheme = FamilyMemberData.GetFamilyMemberScheme();

        var x = await _appDatabaseContext.FamilyMembers.ToListAsync();

        await _appDatabaseContext.AddAsync(familyMemberScheme);
        await _appDatabaseContext.SaveChangesAsync();

        var updateFamilyMemberInput = new UpdateFamilyMemberInput()
        {
            Id = shouldSendValidId ? familyMemberScheme.Id : Guid.NewGuid(),
            Version = shouldSendValidVersion ? familyMemberScheme.Version : [0, 1, 2, 3, 4, 5, 6, 7],
            FirstName = shouldSendValidInput ? "Name" : "",
            LastName = shouldSendValidInput ? "Name2" : "",
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
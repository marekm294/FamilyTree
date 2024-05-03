using Shared.Helpers;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Models.Outputs;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using IntegrationTests.TestsClasses.FamilyMembers.Data;
using IntegrationTests.TestsClasses.Families.Data;
using Shared.Types;

namespace IntegrationTests.TestsClasses.FamilyMembers;

public partial class FamilyMembersTests
{
    [Fact]
    public async Task Update_Family_Member_Success_Async()
    {
        //Arrange
        var familyScheme = FamilyData.GetFamilyScheme();
        await _appDatabaseContext.AddAsync(familyScheme);

        var familyMemberScheme = FamilyMemberData.GetFamilyMemberScheme();
        familyMemberScheme.MiddleNames = ["Marek", "Honza"];
        familyMemberScheme.FamilyId = familyScheme.Id;

        await _appDatabaseContext.AddAsync(familyMemberScheme);
        await _appDatabaseContext.SaveChangesAsync();

        var updateFamilyMemberInput = new UpdateFamilyMemberInput()
        {
            Id = familyMemberScheme.Id,
            Version = familyMemberScheme.Version,
            FirstName = "Marek",
            LastName = "Mička",
            MiddleNames = ["Marek", "Honza", "Pavel"],
            Birth = new Event()
            {
                Date = new DateTime(1997, 4, 30),
                Place = "Praha",
            },
            DeathDate = null
        };

        //Act
        var response = await _httpClient.PutAsJsonAsync(
            FAMILY_MEMBERS_API,
            updateFamilyMemberInput,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        response.EnsureSuccessStatusCode();
        using var content = await response.Content.ReadAsStreamAsync();
        var familyMemberOutput = JsonSerializer.Deserialize<FamilyMemberOutput>(
            content,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEqual(Guid.Empty, familyMemberOutput!.Id);
        Assert.Equal(updateFamilyMemberInput.FirstName, familyMemberOutput!.FirstName);
        Assert.Equal(updateFamilyMemberInput.LastName, familyMemberOutput!.LastName);
        Assert.Equal(updateFamilyMemberInput.MiddleNames.Length, familyMemberOutput!.MiddleNames.Length);
        // event is updated
        Assert.NotEqual(updateFamilyMemberInput.Birth.Date, familyMemberScheme.Birth.Date);
        Assert.NotEqual(updateFamilyMemberInput.Birth.Place, familyMemberScheme.Birth.Place);
        Assert.Equal(updateFamilyMemberInput.Birth.Date, familyMemberOutput.Birth.Date);
        Assert.Equal(updateFamilyMemberInput.Birth.Place, familyMemberOutput.Birth.Place);
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
        var familyScheme = FamilyData.GetFamilyScheme();
        await _appDatabaseContext.AddAsync(familyScheme);

        var familyMemberScheme = FamilyMemberData.GetFamilyMemberScheme();
        familyMemberScheme.FamilyId = familyScheme.Id;

        await _appDatabaseContext.AddAsync(familyMemberScheme);
        await _appDatabaseContext.SaveChangesAsync();

        var updateFamilyMemberInput = new UpdateFamilyMemberInput()
        {
            Id = shouldSendValidId ? familyMemberScheme.Id : Guid.NewGuid(),
            Version = shouldSendValidVersion ? familyMemberScheme.Version : [0, 1, 2, 3, 4, 5, 6, 7],
            FirstName = shouldSendValidInput ? "Name" : "",
            LastName = shouldSendValidInput ? "Name2" : "",
            Birth = new Event()
            {
                Date = new DateTime(1997, 4, 29),
                Place = "Opava",
            },
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
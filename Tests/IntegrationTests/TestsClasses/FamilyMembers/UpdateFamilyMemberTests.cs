using IntegrationTests.TestsClasses.Families.Data;
using IntegrationTests.TestsClasses.FamilyMembers.Data;
using NetTopologySuite.Geometries;
using Shared.Helpers;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Models.Outputs;
using Shared.Types;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace IntegrationTests.TestsClasses.FamilyMembers;

public partial class FamilyMembersTests
{
    [Fact]
    public async Task UpdateFamilyMember_ShouldReturnOk_WhenValidInputIsSent_Async()
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
                Date = new DateOnly(1997, 4, 30),
                Place = new Place()
                {
                    Country = "USA",
                    City = "Opava",
                    Coordinates = new Point(17.904444, 49.938056),
                },
            },
            Death = new Event()
            {
                Date = null,
                Place = new Place(),
            },
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
        Assert.NotEqual(updateFamilyMemberInput.Birth.Place.Country, familyMemberScheme.Birth.Place.Country);
        Assert.Equal(updateFamilyMemberInput.Birth.Date, familyMemberOutput.Birth.Date);
        Assert.Equal(updateFamilyMemberInput.Birth.Place.Country, familyMemberOutput.Birth.Place.Country);
    }

    [Theory]
    [InlineData(true, true, false, true, true, 0, HttpStatusCode.BadRequest)]
    [InlineData(true, true, true, false, true, 2, HttpStatusCode.BadRequest)]
    [InlineData(true, false, true, true, true, 0, HttpStatusCode.NotFound)]
    [InlineData(false, true, true, true, true, 0, HttpStatusCode.NotFound)]
    public async Task UpdateFamilyMember_ShouldReturnUnsuccessfulCode_WhenInvalidApiCallIsMade_Async(
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
                Date = new DateOnly(1997, 4, 29),
                Place = new Place()
                {
                    Country = "Czech Republic",
                    City = "Opava",
                },
            },
            Death = new Event()
            {
                Date = null,
                Place = new Place(),
            },
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
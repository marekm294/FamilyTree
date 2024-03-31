using Shared.Helpers;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Models.Outputs;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace IntegrationTests.TestsClasses.FamilyMembers;

public partial class FamilyMembersTests
{
    [Fact]
    public async Task Create_Family_Member_Success_Async()
    {
        //Arrange
        var createFamilyMemberInput = new CreateFamilyMemberInput()
        {
            FirstName = "Marek",
            LastName = "Mička",
            BirthDate = new DateTime(1997, 4, 29),
            DeathDate = null,
        };

        //Act
        var response = await _httpClient.PostAsJsonAsync(
            FAMILY_MEMBERS_API,
            createFamilyMemberInput,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        response.EnsureSuccessStatusCode();
        using var content = await response.Content.ReadAsStreamAsync();
        var familyMembersOutput = JsonSerializer.Deserialize<FamilyMemberOutput>(
            content,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotEqual(Guid.Empty, familyMembersOutput!.Id);
        Assert.Equal(createFamilyMemberInput.FirstName, familyMembersOutput!.FirstName);
    }

    [Theory]
    [InlineData(false, true, 0, HttpStatusCode.BadRequest)]
    [InlineData(true, true, 2, HttpStatusCode.BadRequest)]
    public async Task Create_Family_Member_Fail_Async(
        bool shouldSendInput,
        bool isErrorOutputExpected,
        int errorCount,
        HttpStatusCode expectedHttpStatusCode)
    {
        //Arrange
        var createFamilyMemberInput = new CreateFamilyMemberInput()
        {
            FirstName = "",
            LastName = "",
            BirthDate = new DateTime(1997, 4, 29),
            DeathDate = null,
        };

        if (shouldSendInput is false)
        {
            createFamilyMemberInput = null;
        }

        //Act
        var response = await _httpClient.PostAsJsonAsync(
            FAMILY_MEMBERS_API,
            createFamilyMemberInput,
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
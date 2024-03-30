using Domain.Entities;
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
}
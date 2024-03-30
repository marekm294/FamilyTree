using Shared.Helpers;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Models.Outputs;
using System.Net.Http.Json;
using System.Net;
using Data.Schemes;
using Domain.Entities;
using Shared.QueryArgs;

namespace IntegrationTests.TestsClasses.FamilyMembers;

public partial class FamilyMembersTests
{
    [Fact]
    public async Task Delete_Family_Member_Success_Async()
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

        var deleteQueryArgs = new DeleteQueryArgs(familyMemberScheme.Id, familyMemberScheme.Version);

        //Act
        var response = await _httpClient.DeleteAsync(
            $"{FAMILY_MEMBERS_API}?{deleteQueryArgs.ToQueryString()}");

        response.EnsureSuccessStatusCode();

        //Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
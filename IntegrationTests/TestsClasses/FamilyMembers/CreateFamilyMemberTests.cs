using Data;
using IntegrationTests.TestsClasses.Families.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        var familyScheme = FamilyData.GetFamilyScheme();
        await _appDatabaseContext.AddAsync(familyScheme);
        await _appDatabaseContext.SaveChangesAsync();

        var createFamilyMemberInput = new CreateFamilyMemberInput()
        {
            FirstName = "Marek",
            LastName = "Mička",
            MiddleNames = [ "Jan", "Pavel" ],
            BirthDate = new DateTime(1997, 4, 29),
            DeathDate = null,
            FamilyId = familyScheme.Id,
        };

        //Act
        var response = await _httpClient.PostAsJsonAsync(
            FAMILY_MEMBERS_API,
            createFamilyMemberInput,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        response.EnsureSuccessStatusCode();
        using var content = await response.Content.ReadAsStreamAsync();
        var familyMemberOutput = JsonSerializer.Deserialize<FamilyMemberOutput>(
            content,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotEqual(Guid.Empty, familyMemberOutput!.Id);
        Assert.Equal(createFamilyMemberInput.FirstName, familyMemberOutput!.FirstName);
        Assert.Equal(createFamilyMemberInput.MiddleNames.Length, familyMemberOutput!.MiddleNames.Length);

        using var assertScope = _serviceScope.ServiceProvider.CreateScope();
        var context = assertScope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
        Assert.True(await context.FamilyMembers.AnyAsync(fm => fm.Id == familyMemberOutput.Id));
    }

    [Theory]
    [InlineData(false, true, true, 0, HttpStatusCode.BadRequest)]
    [InlineData(false, true, false, 0, HttpStatusCode.BadRequest)]
    [InlineData(true, true, true, 2, HttpStatusCode.BadRequest)]
    public async Task Create_Family_Member_Fail_Async(
        bool shouldSendInput,
        bool isErrorOutputExpected,
        bool isFamilyInDb,
        int errorCount,
        HttpStatusCode expectedHttpStatusCode)
    {
        //Arrange
        var familyId = Guid.NewGuid();
        if (isFamilyInDb)
        {
            var familyScheme = FamilyData.GetFamilyScheme();
            await _appDatabaseContext.AddAsync(familyScheme);
            await _appDatabaseContext.SaveChangesAsync();
        }

        var createFamilyMemberInput = new CreateFamilyMemberInput()
        {
            FirstName = "",
            LastName = "",
            BirthDate = new DateTime(1997, 4, 29),
            DeathDate = null,
            FamilyId = familyId,
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
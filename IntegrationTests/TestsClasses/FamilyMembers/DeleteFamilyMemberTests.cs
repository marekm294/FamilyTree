﻿using System.Net;
using Shared.QueryArgs;
using IntegrationTests.TestsClasses.FamilyMembers.Data;
using Shared.Helpers;
using Shared.Models.Outputs;
using System.Text.Json;

namespace IntegrationTests.TestsClasses.FamilyMembers;

public partial class FamilyMembersTests
{
    [Fact]
    public async Task Delete_Family_Member_Success_Async()
    {
        //Arrange
        var familyMemberScheme = FamilyMemberData.GetFamilyMemberScheme();

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

    [Theory]
    [InlineData(true, 1, HttpStatusCode.BadRequest)]
    public async Task Delete_Family_Member_Fail_Async(
        bool isErrorOutputExpected,
        int errorCount,
        HttpStatusCode expectedHttpStatusCode)
    {
        //Arrange
        var familyMemberScheme = FamilyMemberData.GetFamilyMemberScheme();

        await _appDatabaseContext.AddAsync(familyMemberScheme);
        await _appDatabaseContext.SaveChangesAsync();

        var deleteQueryArgs = new DeleteQueryArgs(
            familyMemberScheme.Id, []);

        //Act
        var response = await _httpClient.DeleteAsync(
            $"{FAMILY_MEMBERS_API}?{deleteQueryArgs?.ToQueryString()}");

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
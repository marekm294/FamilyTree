using Data;
using Microsoft.Extensions.DependencyInjection;
using Shared.Helpers;
using Shared.Models.Inputs.Families;
using Shared.Models.Outputs;
using System.Net;
using IntegrationTests.TestsClasses.Families.Data;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using IntegrationTests.TestsClasses.FamilyMembers.Data;
using Shared.Models.Inputs.FamilyMembers;

namespace IntegrationTests.TestsClasses.Families;

public partial class FamiliesTests
{
    [Fact]
    public async Task Update_Family_Success_Async()
    {
        //Arrange
        var familyScheme = FamilyData.GetFamilyScheme();
        await _appDatabaseContext.AddAsync(familyScheme);
        await _appDatabaseContext.SaveChangesAsync();

        var updateFamilyInput = new UpdateFamilyInput()
        {
            Id = familyScheme.Id,
            Version = familyScheme.Version,
            FamilyName = "UpdatedFamilyName",
        };

        //Act
        var response = await _httpClient.PutAsJsonAsync(
            FAMILIES_API,
            updateFamilyInput,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        response.EnsureSuccessStatusCode();
        using var content = await response.Content.ReadAsStreamAsync();
        var familyOutput = JsonSerializer.Deserialize<FamilyOutput>(
            content,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEqual(Guid.Empty, familyOutput!.Id);
        Assert.Equal(updateFamilyInput.FamilyName, familyOutput.FamilyName);

        using var assertScope = _serviceScope.ServiceProvider.CreateScope();
        var context = assertScope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
        Assert.Equal(
            await context.Families.Where(f => f.Id == familyScheme.Id).Select(f => f.FamilyName).FirstAsync(),
            updateFamilyInput.FamilyName);

        Assert.NotEqual(
            await context.Families.Where(f => f.Id == familyScheme.Id).Select(f => f.FamilyName).FirstAsync(),
            familyScheme.FamilyName);
    }

    [Theory]
    [InlineData(true, true, false, true, true, 0, HttpStatusCode.BadRequest)]
    [InlineData(true, true, true, false, true, 1, HttpStatusCode.BadRequest)]
    [InlineData(true, false, true, true, true, 0, HttpStatusCode.NotFound)]
    [InlineData(false, true, true, true, true, 0, HttpStatusCode.NotFound)]
    public async Task Update_Family_Fail_Async(
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
        await _appDatabaseContext.SaveChangesAsync();

        var updateFamilInput = new UpdateFamilyInput()
        {
            Id = shouldSendValidId ? familyScheme.Id : Guid.NewGuid(),
            Version = shouldSendValidVersion ? familyScheme.Version : [0, 1, 2, 3, 4, 5, 6, 7],
            FamilyName = shouldSendValidInput ? "UpdatedFamilyName" : "",
        };

        if (shouldSendInput is false)
        {
            updateFamilInput = null;
        }

        //Act
        var response = await _httpClient.PutAsJsonAsync(
            FAMILIES_API,
            updateFamilInput,
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
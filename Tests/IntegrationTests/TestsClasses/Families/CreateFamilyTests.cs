using Shared.Helpers;
using Shared.Models.Outputs;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Inputs.Families;

namespace IntegrationTests.TestsClasses.Families;

public partial class FamiliesTests
{
    [Fact]
    public async Task Create_Family_Success_Async()
    {
        //Arrange
        var createFamilyInput = new CreateFamilyInput()
        {
            FamilyName =  "FamilyName",
        };
        
        //Act
        var response = await _httpClient.PostAsJsonAsync(
            FAMILIES_API,
            createFamilyInput,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        response.EnsureSuccessStatusCode();
        using var content = await response.Content.ReadAsStreamAsync();
        var familyOutput = JsonSerializer.Deserialize<FamilyOutput>(
            content,
            JsonSerializerHelper.CLIENT_JSON_SERIALIZER_OPTIONS);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotEqual(Guid.Empty, familyOutput!.Id);

        using var assertScope = _serviceScope.ServiceProvider.CreateScope();
        var context = assertScope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
        Assert.True(await context.Families.AnyAsync(fm => fm.Id == familyOutput.Id));
    }

    [Theory]
    [InlineData(false, true, 0, HttpStatusCode.BadRequest)]
    [InlineData(true, true, 1, HttpStatusCode.BadRequest)]
    public async Task Create_Family_Fail_Async(
        bool shouldSendInput,
        bool isErrorOutputExpected,
        int errorCount,
        HttpStatusCode expectedHttpStatusCode)
    {
        //Arrange
        var createFamilyInput = new CreateFamilyInput()
        {
            FamilyName = isErrorOutputExpected ? "" : "NewFamilyName",
        };

        if (shouldSendInput is false)
        {
            createFamilyInput = null;
        }

        //Act
        var response = await _httpClient.PostAsJsonAsync(
            FAMILIES_API,
            createFamilyInput,
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
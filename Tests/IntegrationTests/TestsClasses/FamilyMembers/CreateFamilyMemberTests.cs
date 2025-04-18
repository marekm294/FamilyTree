﻿using Data;
using IntegrationTests.TestsClasses.Families.Data;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using Shared.Helpers;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Models.Outputs;
using Shared.Types;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using SystemTestsCore.Helpers;

namespace IntegrationTests.TestsClasses.FamilyMembers;

public partial class FamilyMembersTests
{
    [Fact]
    public async Task CreateFamilyMember_ShouldReturnCreated_WhenValidInputIsSent_Async()
    {
        //Arrange
        var familyScheme = FamilyData.GetFamilyScheme();
        await _appDatabaseContext.AddAsync(familyScheme);
        await _appDatabaseContext.SaveChangesAsync();

        var createFamilyMemberInput = new CreateFamilyMemberInput()
        {
            FirstName = "Marek",
            LastName = "Mička",
            MiddleNames = ["Jan", "Pavel"],
            Birth = new Event()
            {
                Date = new DateOnly(1997, 4, 29),
                Place = new Place()
                {
                    Country = "Czech Republic",
                    City = "Opava",
                    Coordinates = new Point(17.904444, 49.938056),
                },
            },
            Death = new Event()
            {
                Date = null,
                Place = new Place(),
            },
            FamilyId = familyScheme.Id,
            AboutMember = "He was awsome."
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
        Assert.Equal(createFamilyMemberInput.Birth.Place.Country, familyMemberOutput!.Birth.Place.Country);
        Assert.Equal(createFamilyMemberInput.Birth.Place.Coordinates, familyMemberOutput!.Birth.Place.Coordinates);

        using var assertScope = _serviceScope.ServiceProvider.CreateScope();
        var context = assertScope.ServiceProvider.GetRequiredService<AppDatabaseContext>();

        var familyMemberFromDb = await context
            .FamilyMembers
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(fm => fm.Id == familyMemberOutput.Id);

        Assert.NotNull(familyMemberFromDb);
        Assert.Equal(familyMemberFromDb.TenantId, Constants.TenantId1);
    }

    [Theory]
    [InlineData(false, true, true, 0, HttpStatusCode.BadRequest)]
    [InlineData(false, true, false, 0, HttpStatusCode.BadRequest)]
    [InlineData(true, true, true, 2, HttpStatusCode.BadRequest)]
    public async Task CreateFamilyMember_ShouldReturnUnsuccessfulCode_WhenInvalidApiCallIsMade_Async(
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
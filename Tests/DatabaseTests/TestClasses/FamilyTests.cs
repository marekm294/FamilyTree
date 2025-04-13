using Data;
using Data.Schemes;
using DatabaseTests.TestClasses.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Types;
using SystemTestsCore.Helpers;
using Xunit.Abstractions;

namespace DatabaseTests.TestClasses;

public class FamilyTests : FactoryCollectionTestClass
{
    public FamilyTests(
        DatabaseFixture fixture,
        ITestOutputHelper testOutputHelper)
        : base(fixture, testOutputHelper)
    {
    }

    [Fact]
    public async Task DeleteFamilyDeletesAllFamilyMembersAsync()
    {
        //Arrange
        var family = new FamilyScheme()
        {
            Id = Guid.NewGuid(),
            FamilyName = "FamilyName",
            TenantId = Constants.TenantId1,
        };
        var familyMember1 = new FamilyMemberScheme()
        {
            Id = Guid.NewGuid(),
            FirstName = $"FirstName",
            LastName = $"LastName",
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
                Date = new DateOnly(2058, 9, 21),
                Place = new Place(),
            },
            FamilyId = family.Id,
            TenantId = Constants.TenantId1,
        };

        await _appDatabaseContext.AddAsync(family);
        await _appDatabaseContext.AddAsync(familyMember1);

        await _appDatabaseContext.SaveChangesAsync();

        //Act
        using var actScope = _serviceScope.ServiceProvider.CreateScope();
        var actContext = actScope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
        actContext.Remove(family);
        await actContext.SaveChangesAsync();

        //Assert
        using var assertScope = _serviceScope.ServiceProvider.CreateScope();
        var assertContext = assertScope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
        Assert.False(await assertContext.Families.AnyAsync());
        Assert.False(await assertContext.FamilyMembers.AnyAsync());
    }
}
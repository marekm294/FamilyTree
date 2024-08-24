using Data.Schemes;
using Data.Schemes.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Data;

internal class AppDatabaseContext : DbContext
{
    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FamilyMemberScheme> FamilyMembers { get; set; }
    public virtual DbSet<FamilyScheme> Families { get; set; }
    public virtual DbSet<WeddingScheme> Weddings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDatabaseContext))!);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Modified &&
                entry.Entity is DbScheme dbEntity)
            {
                dbEntity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
using Data.Schemes;
using Data.Schemes.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data;

internal class AppDatabaseContext : DbContext
{
    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FamilyMemberScheme> FamilyMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDatabaseContext))!);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is DbScheme dbEntity && entry.State == EntityState.Modified)
            {
                dbEntity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
using Data.Schemes;
using Data.Schemes.Abstraction;
using Domain.Entities.Abstraction;
using Domain.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Data;

internal class AppDatabaseContext : DbContext
{
    private readonly ICurrentTenant _currentTenant;

    public AppDatabaseContext(
        DbContextOptions<AppDatabaseContext> options,
        ICurrentTenant currentTenant)
        : base(options)
    {
        _currentTenant = currentTenant;
    }

    public virtual DbSet<FamilyMemberScheme> FamilyMembers { get; set; }
    public virtual DbSet<FamilyScheme> Families { get; set; }
    public virtual DbSet<WeddingScheme> Weddings { get; set; }
    public virtual DbSet<TenantScheme> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDatabaseContext))!);
        AddQueryFilters(modelBuilder);
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

    private void AddQueryFilters(ModelBuilder modelBuilder)
    {
        var entityTypes = Assembly
            .GetAssembly(typeof(DbScheme))?
            .GetTypes()?
            .Where(t => t.IsAssignableTo(typeof(ITenantable)) && t.IsAbstract is false);

        if (entityTypes is null)
        {
            return;
        }

        foreach (var entityType in entityTypes)
        {
            typeof(AppDatabaseContext)
                .GetMethod(nameof(AddQueryFilter), BindingFlags.NonPublic | BindingFlags.Instance)?
                .MakeGenericMethod(entityType)?
                .Invoke(this, [modelBuilder]);
        }
    }

    private void AddQueryFilter<TEntity>(ModelBuilder modelBuilder)
        where TEntity : class, ITenantable
    {
        modelBuilder
            .Entity<TEntity>()
            .HasQueryFilter(e => e.TenantId == _currentTenant.TenantId);
    }
}
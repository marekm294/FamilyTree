using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data;

internal sealed class AppDatabaseContext : DbContext
{
    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDatabaseContext))!);

        base.OnModelCreating(modelBuilder);
    }
}
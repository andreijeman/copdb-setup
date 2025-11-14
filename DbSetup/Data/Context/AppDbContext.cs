using DbSetup.Data.Entities;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DbSetup.Data.Context;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();

            if (!string.IsNullOrEmpty(tableName))
            {
                entity.SetTableName("t_" + tableName.Underscore());
            }
        }
    }

    public DbSet<ReferenceBank> ReferenceBanks { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<SystemLog> SystemLogs { get; set; }
}
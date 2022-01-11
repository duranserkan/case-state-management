using Microsoft.EntityFrameworkCore;
using StateManagement.SharedKernel;

namespace StateManagement.Infrastructure.Repositories;

public class DbContextBase : DbContext
{
    public DbContextBase(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<IDomainEvent>();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateAudit();

        var result = base.SaveChanges(acceptAllChangesOnSuccess);

        PublishDomainEvents().RunSynchronously();

        return result;
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateAudit();

        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        await PublishDomainEvents();

        return result;
    }

    private void UpdateAudit()
    {
        var utcNow = DateTime.UtcNow;

        _ = ChangeTracker.Entries()
            .Where(entityEntry =>
                entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified || entityEntry.State == EntityState.Deleted)
            .Select(entityEntry =>
            {
                if (entityEntry.Entity is IUpdatedOn u)
                {
                    u.UpdatedOn = utcNow;
                }
                if (entityEntry.Entity is ICreatedOn c)
                {
                    c.CreatedOn = utcNow;
                }
                return entityEntry;
            }).ToArray();
    }
    
    private async Task PublishDomainEvents()
    {
        // todo: masstransit can publish domain events to registered transport such as rabbitmq
    }
}
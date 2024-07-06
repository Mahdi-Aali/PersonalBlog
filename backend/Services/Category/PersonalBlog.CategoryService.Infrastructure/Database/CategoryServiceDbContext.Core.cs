using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalBlog.BuildingBlocks.Extenssions.Tasks;
using PersonalBlog.CategoryService.Domain.SeedWorker;

namespace PersonalBlog.CategoryService.Infrastructure.Database;

public sealed partial class CategoryServiceDbContext : IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public async Task<int> SaveEntitiesAsync(IProgress<string>? progress, CancellationToken cancellationToken = default)
    {
        try
        {
            int domainEventsCount = ChangeTracker.Entries<Entity>().SelectMany(x => x.Entity.DomainEvents).Count();
            int publishedDomainEvents = 0;
            foreach (var entity in ChangeTracker.Entries<Entity>())
            {
                foreach (var domainEvent in entity.Entity.DomainEvents)
                {
                    await _publisher.Publish(domainEvent, cancellationToken).WithTimeout(TimeSpan.FromSeconds(2));
                    progress?.Report($"{publishedDomainEvents} domain event(s) publilshed from {domainEventsCount} domain event(s).");
                }
            }
            return await SaveChangesAsync(cancellationToken).WithTimeout(TimeSpan.FromSeconds(5));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "there is an error when saving and publishing domain events.");
            return -1;
        }
    }
}

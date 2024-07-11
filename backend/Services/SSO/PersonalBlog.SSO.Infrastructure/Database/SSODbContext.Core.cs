using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalBlog.SSO.Domain.SeedWorker;
using System.Text.Json;

namespace PersonalBlog.SSO.Infrastructure.Database;

public partial class SSODbContext : IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }


    public async Task<int> SaveEntitiesAsync(IProgress<string>? progress, CancellationToken cancellationToken = default)
    {
        try
        {
            foreach(var entity in ChangeTracker.Entries<ISSOEntity>())
            {
                entity.Entity.DomainEvents().ToList().ForEach(async evn =>
                {
                    await _publisher.Publish(evn, cancellationToken);
                    progress?.Report($"{JsonSerializer.Serialize(evn)} published.");
                });
            }

            return await SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "something went wrong when saving and publishing domain events.");
            return -1;
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;
using PersonalBlog.CategoryService.Domain.SeedWorker;

namespace PersonalBlog.CategoryService.Infrastructure.Database;

public sealed partial class CategoryServiceDbContext : DbContext, IUnitOfWork
{
    public const string SCHEMA = "category";

    private IPublisher _publisher;

    public CategoryServiceDbContext(DbContextOptions<CategoryServiceDbContext> options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<Category> Categories => Set<Category>();

    public Task<int> SaveEntitiesAsync(IProgress<int>? progress, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
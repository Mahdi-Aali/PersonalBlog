using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;

namespace PersonalBlog.CategoryService.Infrastructure.Database;

public sealed partial class CategoryServiceDbContext : DbContext
{
    public const string SCHEMA = "category";

    private IPublisher _publisher;
    private ILogger<CategoryServiceDbContext> _logger;

    public CategoryServiceDbContext(DbContextOptions<CategoryServiceDbContext> options, IPublisher publisher, ILogger<CategoryServiceDbContext> logger) : base(options)
    {
        _publisher = publisher;
        _logger = logger;
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<CategoryVisibilityStatus> categoryVisibilityStatuses => Set<CategoryVisibilityStatus>();

}
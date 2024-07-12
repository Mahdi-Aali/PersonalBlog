using PersonalBlog.BuildingBlocks.Logging.Contracts;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;
using PersonalBlog.CategoryService.Domain.SeedWorker;
using PersonalBlog.CategoryService.Infrastructure.Database;

namespace PersonalBlog.CategoryService.Infrastructure.Repositories.CategoryAggregate
{
    public class DefaultCategoryDbContextHandler : DbContextHandlerBase<Category, CategoryServiceDbContext>
    {
        public DefaultCategoryDbContextHandler(CategoryServiceDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}

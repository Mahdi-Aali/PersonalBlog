using Microsoft.Extensions.Logging;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;
using PersonalBlog.CategoryService.Domain.SeedWorker;
using PersonalBlog.CategoryService.Infrastructure.Database;

namespace PersonalBlog.CategoryService.Infrastructure.Repositories.CategoryAggregate
{
    public class DefaultCategoryDbContextHandler : DbContextHandlerBase<Category, CategoryServiceDbContext>
    {
        public DefaultCategoryDbContextHandler(CategoryServiceDbContext context, ILogger<IRepository> logger) : base(context, logger)
        {
        }
    }
}

using Microsoft.Extensions.Logging;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate.DomainEvents;
using PersonalBlog.CategoryService.Domain.SeedWorker;
using PersonalBlog.CategoryService.Infrastructure.Database;
using System.Linq.Expressions;

namespace PersonalBlog.CategoryService.Infrastructure.Repositories.CategoryAggregate;

public class CategoryRepository : ICategoryRepository
{
    private readonly ILogger<CategoryRepository> _logger;
    private readonly DbContextHandlerBase<Category, CategoryServiceDbContext> _dbContextHandler;

    public CategoryRepository(ILogger<CategoryRepository> logger, DbContextHandlerBase<Category, CategoryServiceDbContext> dbContextHandler)
    {
        _logger = logger;
        _dbContextHandler = dbContextHandler;

    }


    public IUnitOfWork UnitOfWork => _dbContextHandler.GetContext();


    public async Task<Category> AddAsync(Category entity, CancellationToken cancellationToken = default)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity), "category can't be null when inserting to database.");
        }
        entity.AddDomainEvents(new CategoryInsertedToDatabaseEvent(entity.Id, entity.Title, entity.CategoryVisibilityStatusId));
        return await _dbContextHandler.AddAsync(entity, cancellationToken);
    }

    public async Task<AggregatePagedResult<IEnumerable<Category>>> CategoriesAsync(AggregatePagedResultSettings settings, CancellationToken cancellationToken = default)
    {
        return new AggregatePagedResult<IEnumerable<Category>>(settings.PageId, settings.ItemPerPage, await CountAsync(),
            (await _dbContextHandler.SetAsync())
            .OrderByDescending(key => key.CreatedDate)
            .ThenByDescending(key => key.UpdatedDate)
            .Skip(settings.PageId * settings.ItemPerPage)
            .Take(settings.ItemPerPage));
    }

    public async Task<AggregatePagedResult<IEnumerable<Category>>> CategoriesAsync(Expression<Func<Category, bool>> queryExpression, AggregatePagedResultSettings settings, CancellationToken cancellationToken = default)
    {
        return new AggregatePagedResult<IEnumerable<Category>>(settings.PageId, settings.ItemPerPage, await CountAsync(queryExpression, cancellationToken),
            (await _dbContextHandler.SetAsync()).Where(queryExpression)
            .OrderByDescending(key => key.CreatedDate)
            .ThenByDescending(key => key.UpdatedDate)
            .Skip(settings.PageId * settings.ItemPerPage).Take(settings.ItemPerPage));
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContextHandler.CountAsync(cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<Category, bool>> queryExpression, CancellationToken cancellationToken = default)
    {
        return await _dbContextHandler.QueryableCountAsync(queryExpression, cancellationToken);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalBlog.BuildingBlocks.Extenssions.Tasks;
using PersonalBlog.CategoryService.Domain.SeedWorker;
using System.Linq.Expressions;

namespace PersonalBlog.CategoryService.Infrastructure.Database;

public abstract class DbContextHandlerBase<TEntity, TDbContext> where TEntity : Entity where TDbContext : DbContext, IUnitOfWork
{
    private TDbContext _context;
    private ILogger<IRepository> _logger;

    protected DbContextHandlerBase(TDbContext context, ILogger<IRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public TDbContext GetContext() => _context;

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "entity can't be null");
            }
            return (await _context.AddAsync(entity, cancellationToken)).Entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "entity can't be null");
            return null!;
        }
    }

    public virtual async Task<IQueryable<TEntity>> SetAsync()
    {
        try
        {
            return await Task.Run(() =>
            {
                return _context.Set<TEntity>();
            })
           .WithTimeout(TimeSpan.FromSeconds(5));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "failed to fetch data from database.");
            return Enumerable.Empty<TEntity>().AsQueryable();
        }
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await (await SetAsync()).CountAsync(cancellationToken).WithTimeout(TimeSpan.FromSeconds(3));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "failed to cound data.");
            return 0;
        }
    }

    public virtual async Task<int> QueryableCountAsync(Expression<Func<TEntity, bool>> queryExpression, CancellationToken cancellationToken = default)
    {
        try
        {
            return await (await SetAsync()).Where(queryExpression).CountAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "failed to count data.");
            return 0;
        }
    }

    public virtual async Task<long> LongCountAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await (await SetAsync()).LongCountAsync(cancellationToken).WithTimeout(TimeSpan.FromSeconds(3));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "failed to count data.");
            return 0;
        }
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            return await Task.Run(() =>
            {
                _context.Update(entity);
                return entity;
            })
            .WithTimeout(TimeSpan.FromSeconds(5));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "failed to update data.");
            return null!;
        }
    }

    public virtual async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            try
            {
                _context.Remove(entity);
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to delete data.");
                return -1;
            }
        });
    }
}

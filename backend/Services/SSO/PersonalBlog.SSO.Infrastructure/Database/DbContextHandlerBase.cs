using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalBlog.SSO.Domain.SeedWorker;

namespace PersonalBlog.SSO.Infrastructure.Database;

public abstract class DbContextHandlerBase<TEntity, TDbContext> 
    where TEntity : class, ISSOEntity 
    where TDbContext : DbContext, IUnitOfWork
{
    private readonly TDbContext _context;
    private readonly ILogger<IRepository> _logger;


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
            ArgumentNullException.ThrowIfNull(entity);
            return (await _context.AddAsync(entity, cancellationToken)).Entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something when wrong when adding entity to database.");
            return null!;
        }
    }

    public virtual async Task<IQueryable<TEntity>> SetAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            try
            {
                return _context.Set<TEntity>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "something went wrong when fetching data from database.");
                return Enumerable.Empty<TEntity>().AsQueryable();
            }
        });
        
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
                _logger.LogError(ex, "something went wrong when deleteing the entity from database.");
                return -1;
            }
        });
    }


    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            try
            {
                return _context.Update(entity).Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "something went wrong when updateing eneity.");
                return null!;
            }
        });
    }
}

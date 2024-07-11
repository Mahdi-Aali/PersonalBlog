using System.Linq.Expressions;

namespace PersonalBlog.ApiKeyRepository.Domain.SeedWorker.RepositoryBase;

public interface IPagedQueryableListAsync<TResult> where TResult : Entity
{
    public Task<AggregatePagedResult<IEnumerable<TResult>>> CategoriesAsync(Expression<Func<TResult, bool>> queryExpression, AggregatePagedResultSettings settings, CancellationToken cancellationToken = default);
}

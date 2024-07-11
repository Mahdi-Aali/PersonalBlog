using System.Linq.Expressions;
using System.Numerics;

namespace PersonalBlog.ApiKeyRepository.Domain.SeedWorker.RepositoryBase;

public interface IQueryableEntityCountAsync<TResult, TEntity> where TEntity : Entity where TResult : INumber<TResult>
{
    public Task<TResult> CountAsync(Expression<Func<TEntity, bool>> queryExpression, CancellationToken cancellationToken = default);
}

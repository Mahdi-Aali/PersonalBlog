using System.Numerics;

namespace PersonalBlog.ApiKeyRepository.Domain.SeedWorker.RepositoryBase;

public interface IEntityCountAsync<TResult> where TResult : INumber<TResult>
{
    public Task<TResult> CountAsync(CancellationToken cancellationToken = default);
}

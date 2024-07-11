namespace PersonalBlog.ApiKeyRepository.Domain.SeedWorker.RepositoryBase;

public interface IAddAsync<TResult, TEntity> where TEntity : Entity
{
    public Task<TResult> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
}

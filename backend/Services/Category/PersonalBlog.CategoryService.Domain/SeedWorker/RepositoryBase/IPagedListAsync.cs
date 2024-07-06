namespace PersonalBlog.CategoryService.Domain.SeedWorker.RepositoryBase;

public interface IPagedListAsync<TResult> where TResult : Entity
{
    public Task<AggregatePagedResult<IEnumerable<TResult>>> CategoriesAsync(AggregatePagedResultSettings settings, CancellationToken cancellationToken = default);
}

namespace PersonalBlog.CategoryService.Domain.SeedWorker;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public Task<int> SaveEntitiesAsync(IProgress<int>? progress, CancellationToken cancellationToken = default);
}
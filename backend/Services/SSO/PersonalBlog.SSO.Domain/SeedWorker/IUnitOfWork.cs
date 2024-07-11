namespace PersonalBlog.SSO.Domain.SeedWorker;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public Task<int> SaveEntitiesAsync(IProgress<string>? progress, CancellationToken cancellationToken = default);
}
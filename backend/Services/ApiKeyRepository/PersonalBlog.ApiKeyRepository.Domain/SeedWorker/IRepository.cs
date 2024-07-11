namespace PersonalBlog.ApiKeyRepository.Domain.SeedWorker;

public interface IRepository
{
    public IUnitOfWork UnitOfWork { get; }
}

namespace PersonalBlog.SSO.Domain.SeedWorker;

public interface IRepository
{
    public IUnitOfWork UnitOfWork { get; }
}

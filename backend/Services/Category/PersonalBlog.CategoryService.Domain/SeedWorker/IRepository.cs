namespace PersonalBlog.CategoryService.Domain.SeedWorker;

public interface IRepository
{
    public IUnitOfWork UnitOfWork { get; }
}

using PersonalBlog.CategoryService.Domain.SeedWorker;
using PersonalBlog.CategoryService.Domain.SeedWorker.RepositoryBase;
using System.Linq.Expressions;

namespace PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;

public interface ICategoryRepository : 
    IRepository<Category>,
    IPagedListAsync<Category>,
    IPagedQueryableListAsync<Category>,
    IAddAsync<Category, Category>,
    IEntityCountAsync<int>,
    IQueryableEntityCountAsync<int, Category>
{
}

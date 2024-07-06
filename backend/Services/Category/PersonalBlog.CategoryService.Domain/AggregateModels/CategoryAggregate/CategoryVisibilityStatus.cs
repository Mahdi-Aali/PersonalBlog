using PersonalBlog.CategoryService.Domain.SeedWorker;

namespace PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;

public class CategoryVisibilityStatus : Enumeration
{
    public CategoryVisibilityStatus(int id, string title) : base(id, title)
    {
    }


    public static CategoryVisibilityStatus Enable => new (1, nameof(Enable));
    public static CategoryVisibilityStatus Disable => new(2, nameof(Disable));
}
using PersonalBlog.CategoryService.Domain.SeedWorker;

namespace PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;

public partial class Category : Entity, ICreatedDate, IUpdatedDate
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CategoryVisibilityStatusId { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime CreatedDate { get; set; }

    public CategoryVisibilityStatus CategoryVisibilityStatus { get; set; }


    protected Category()
    {
    }   
}
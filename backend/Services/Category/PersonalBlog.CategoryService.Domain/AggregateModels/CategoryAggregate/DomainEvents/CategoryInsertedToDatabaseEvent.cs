using MediatR;

namespace PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate.DomainEvents;

public class CategoryInsertedToDatabaseEvent : INotification
{
    public CategoryInsertedToDatabaseEvent(Guid categoryId, string title, int visibilityStatus)
    {
        CategoryId = categoryId;
        Title = title;
        VisibilityStatus = visibilityStatus;
    }

    public Guid CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int VisibilityStatus { get; set; }

}

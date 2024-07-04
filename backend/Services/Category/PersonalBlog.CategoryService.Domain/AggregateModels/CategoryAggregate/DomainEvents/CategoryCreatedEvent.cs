using MediatR;

namespace PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate.DomainEvents;

public class CategoryCreatedEvent : INotification
{
    public CategoryCreatedEvent(Guid categoryId, string categoryNamee, DateTime categoryCreatedDateTime)
    {
        CategoryId = categoryId;
        CategoryNamee = categoryNamee;
        CategoryCreatedDateTime = categoryCreatedDateTime;
    }

    public Guid CategoryId { get; set; }
    public string CategoryNamee { get; set; } = string.Empty;
    public DateTime CategoryCreatedDateTime { get; set; }
}
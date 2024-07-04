namespace PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate.LocalEventArgs;

public class CategoryCreatedEventArgs : EventArgs
{
    public CategoryCreatedEventArgs(string categoryTitle, DateTime createdDateTime)
    {
        CategoryTitle = categoryTitle;
        CreatedDateTime = createdDateTime;
    }

    public string CategoryTitle { get; set; } = string.Empty;

    public DateTime CreatedDateTime { get; set; }
}
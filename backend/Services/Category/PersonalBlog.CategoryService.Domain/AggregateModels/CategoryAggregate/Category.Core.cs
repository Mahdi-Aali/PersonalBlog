using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate.DomainEvents;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate.LocalEventArgs;

namespace PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate;

public partial class Category
{
    public event EventHandler<CategoryCreatedEventArgs>? OnCategoryCreated;

    public override void HandleEvents<TEventArgs>(TEventArgs e)
    {
        switch (e)
        {
            case CategoryCreatedEventArgs categoryCreatedEventArgs:
                {
                    if (OnCategoryCreated != null)
                    {
                        OnCategoryCreated.Invoke(this, categoryCreatedEventArgs);
                    }
                    break;
                }
        }
    }

    public static CategoryFactory Factory => new CategoryFactory();

    public class CategoryFactory
    {
        public Category Create(string title, string description)
        {
            Category category = new Category()
            {
                Title = title,
                Description = description,
                CategoryVisibilityStatusId = CategoryVisibilityStatus.Enable.Id,
                CreatedDate = DateTime.Now
            };
            category.AddDomainEvents(new CategoryCreatedEvent(category.Id, category.Title, category.CreatedDate));
            category.HandleEvents(new CategoryCreatedEventArgs(category.Title, category.CreatedDate));
            return category;
        }
    }

}

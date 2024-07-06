using MediatR;
using Microsoft.Extensions.Logging;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate.DomainEvents;
using System.Text.Json;

namespace PersonalBlog.CategoryService.Application.NotificationHandlers.CategoryNotificationHandlers;

public class CategoryInsertedToDatabaseNotificationHandler : INotificationHandler<CategoryInsertedToDatabaseEvent>
{
    private readonly ILogger<CategoryInsertedToDatabaseNotificationHandler> _logger;

    public CategoryInsertedToDatabaseNotificationHandler(ILogger<CategoryInsertedToDatabaseNotificationHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(CategoryInsertedToDatabaseEvent notification, CancellationToken cancellationToken)
    {
        await Task.Run(() => { _logger.LogInformation(JsonSerializer.Serialize(notification)); });
    }
}

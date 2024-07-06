using MediatR;
using Microsoft.Extensions.Logging;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate.DomainEvents;
using System.Text.Json;

namespace PersonalBlog.CategoryService.Application.NotificationHandlers.CategoryNotificationHandlers;

public class CategoryCreatedNotificationHandler : INotificationHandler<CategoryCreatedEvent>
{
    private ILogger<CategoryCreatedNotificationHandler> _logger;

    public CategoryCreatedNotificationHandler(ILogger<CategoryCreatedNotificationHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
    {
        await Task.Run(() => { _logger.LogInformation(JsonSerializer.Serialize(notification)); });
    }
}

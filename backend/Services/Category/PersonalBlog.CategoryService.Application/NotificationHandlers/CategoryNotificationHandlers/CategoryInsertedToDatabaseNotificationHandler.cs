using MediatR;
using PersonalBlog.BuildingBlocks.Logging.Contracts;
using PersonalBlog.CategoryService.Domain.AggregateModels.CategoryAggregate.DomainEvents;
using System.Text.Json;

namespace PersonalBlog.CategoryService.Application.NotificationHandlers.CategoryNotificationHandlers;

public class CategoryInsertedToDatabaseNotificationHandler : INotificationHandler<CategoryInsertedToDatabaseEvent>
{
    private readonly ILogger _logger;

    public CategoryInsertedToDatabaseNotificationHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async Task Handle(CategoryInsertedToDatabaseEvent notification, CancellationToken cancellationToken)
    {
        await Task.Run(async () => { await _logger.LogInformation(JsonSerializer.Serialize(notification)); });
    }
}

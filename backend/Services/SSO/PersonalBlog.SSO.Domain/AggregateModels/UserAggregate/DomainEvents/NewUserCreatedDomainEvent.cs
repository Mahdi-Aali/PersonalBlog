using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace PersonalBlog.SSO.Domain.AggregateModels.UserAggregate.DomainEvents;

public class NewUserCreatedDomainEvent : INotification
{
    public required Guid UserId { get; set; }
    public required string Username { get; set; } = string.Empty;


    [SetsRequiredMembers]
    public NewUserCreatedDomainEvent(Guid userId, string username)
    {
        UserId = userId;
        Username = username;
    }
}

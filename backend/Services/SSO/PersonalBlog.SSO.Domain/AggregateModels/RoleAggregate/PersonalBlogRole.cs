using MediatR;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.SSO.Domain.AggregateModels.UserAggregate;
using PersonalBlog.SSO.Domain.SeedWorker;

namespace PersonalBlog.SSO.Domain.AggregateModels.RoleAggregate;

public class PersonalBlogRole : IdentityRole<Guid>, ISSOEntity, ICreatedDate, IUpdatedDate
{
    private List<INotification> _domainEvents = new();
    public DateTime CreatedData { get; set; }
    public DateTime? UpdatedDate { get; set; }



    public void AddDomainEvent(INotification notification) => _domainEvents.Add(notification);

    public void ClearDomainEvents() => _domainEvents.Clear();

    public IReadOnlyCollection<INotification> DomainEvents() => _domainEvents.AsReadOnly();
}

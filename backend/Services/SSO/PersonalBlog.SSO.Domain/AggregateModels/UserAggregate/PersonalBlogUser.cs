using MediatR;
using Microsoft.AspNetCore.Identity;
using PersonalBlog.SSO.Domain.SeedWorker;

namespace PersonalBlog.SSO.Domain.AggregateModels.UserAggregate;

public class PersonalBlogUser : IdentityUser<Guid>, ISSOEntity, ICreatedDate, IUpdatedDate
{
    private List<INotification> _domainEvents = new();
    public int UserAccountStatusId { get; set; }
    public DateTime CreatedData { get; set; }
    public DateTime? UpdatedDate { get; set; }


    public UserAccountStatus UserAccountStatus { get; set; } = null!;



    public void AddDomainEvent(INotification notification) => _domainEvents.Add(notification);

    public void ClearDomainEvents() => _domainEvents.Clear();

    public IReadOnlyCollection<INotification> DomainEvents() => _domainEvents.AsReadOnly();
}

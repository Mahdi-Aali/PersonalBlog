using MediatR;

namespace PersonalBlog.SSO.Domain.SeedWorker;

public interface ISSOEntity
{
    public IReadOnlyCollection<INotification> DomainEvents();

    public void AddDomainEvent(INotification notification);
    public void ClearDomainEvents();
}

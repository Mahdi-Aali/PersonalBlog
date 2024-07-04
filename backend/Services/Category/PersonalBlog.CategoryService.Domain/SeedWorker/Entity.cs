using MediatR;

namespace PersonalBlog.CategoryService.Domain.SeedWorker;

public abstract class Entity
{
    private List<INotification> _domainEvents;

    public Guid Id { get; private set; }
    public Guid ConcurencyToken { get; set; }

    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents ?? new List<INotification>();

    protected Entity()
    {
        Id = Guid.NewGuid();
        ConcurencyToken = Guid.NewGuid();
        _domainEvents = new();
    }


    public void AddDomainEvents(INotification notification)
    {
        _domainEvents.Add(notification);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }


    public delegate void EventHandler<TEventArg>(object sender, TEventArg e) where TEventArg : EventArgs;

    public abstract void HandleEvents<TEventArgs>(TEventArgs e) where TEventArgs : EventArgs;
}
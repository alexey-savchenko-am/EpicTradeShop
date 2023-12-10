namespace SharedKernel;

public abstract class AggregateRoot
    : Entity, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(Guid id) 
        : base(id) {}

    public IReadOnlyCollection<IDomainEvent> DomainEvents 
        => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
        => this._domainEvents.Clear();

    public void RaiseDomainEvent(IDomainEvent domainEvent)
        => this._domainEvents.Add(domainEvent);
}

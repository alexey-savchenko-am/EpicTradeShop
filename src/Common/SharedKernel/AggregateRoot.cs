namespace SharedKernel;

public abstract class AggregateRoot
    : GuidKeyEntity, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(ID id) 
        : base(id) {}

    public IReadOnlyCollection<IDomainEvent> DomainEvents 
        => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
        => _domainEvents.Clear();

    public void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);
}

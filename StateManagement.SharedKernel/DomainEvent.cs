namespace StateManagement.SharedKernel;

public interface IDomainEvent
{
    string? EntityName { get; }
    Guid EventId { get; }
    DateTime EventTime { get; }
}

public interface IDomainEvent<TId> : IDomainEvent
{
    TId EntityId { get; }
}

public abstract class DomainEvent<TId> : IDomainEvent<TId>
{
    private readonly IEntityId<TId> _entityId;

    protected DomainEvent(IEntityId<TId> entityId)
    {
        _entityId = entityId;
    }

    public string? EntityName => _entityId.EntityName;
    public TId EntityId => _entityId.Id;
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime EventTime => _entityId.Entity.UpdatedOn ?? DateTime.UtcNow;
}
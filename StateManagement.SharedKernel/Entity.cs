namespace StateManagement.SharedKernel;

public interface IUpdatedOn
{
    DateTime? UpdatedOn { get; set; }
}

public interface ICreatedOn
{
    DateTime CreatedOn { get; set; }
}


public interface IEntity<TId> : ICreatedOn, IUpdatedOn
{
    TId Id { get; }
    IEntityId<TId> EntityId { get; }
}


public abstract class Entity : ICreatedOn, IUpdatedOn
{
    protected List<IDomainEvent> Events { get; } = new();
    public DateTime? UpdatedOn { get; set; }
    public DateTime CreatedOn { get; set; }
}

public abstract class Entity<TId> : Entity, IEntity<TId>
{
    public TId Id { get; protected set; }
    public IEntityId<TId> EntityId => new EntityId<TId>(this);

    protected bool Equals(Entity<TId> other)
    {
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Entity<TId>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId>? right)
    {
        return !Equals(left, right);
    }
}
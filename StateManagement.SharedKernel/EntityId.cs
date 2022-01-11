namespace StateManagement.SharedKernel;

public interface IEntityId<TId>
{
    IEntity<TId> Entity { get; }
    TId Id { get; }
    string? EntityName { get; }
}

public class EntityId<TId> : IEntityId<TId>
{
    public EntityId(IEntity<TId> entity)
    {
        Entity = entity;
    }

    public TId Id => Entity.Id;
    public IEntity<TId> Entity { get; }

    public string? EntityName => Entity.GetType().FullName;

    protected bool Equals(EntityId<TId> other)
    {
        return Entity.Equals(other.Entity);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((EntityId<TId>)obj);
    }

    public override int GetHashCode()
    {
        return Entity.GetHashCode();
    }

    public static bool operator ==(EntityId<TId> left, EntityId<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EntityId<TId> left, EntityId<TId> right)
    {
        return !Equals(left, right);
    }
}
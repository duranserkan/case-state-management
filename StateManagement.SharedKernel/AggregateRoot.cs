namespace StateManagement.SharedKernel;

public interface IAggregateRoot<TId> : IEntity<TId> { }

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId> { }
using StateManagement.Contract.Task.DomainEvents;
using StateManagement.SharedKernel;

namespace StateManagement.Domain.Task.DomainEvents;

public class TaskCreated : DomainEvent<long>, ITaskCreated
{
    public TaskCreated(IEntityId<long> entityId) : base(entityId)
    {
    }
}
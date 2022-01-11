using StateManagement.Contract.Task.DomainEvents;
using StateManagement.SharedKernel;

namespace StateManagement.Domain.Flow.DomainEvents;

public class FlowCreated : DomainEvent<long>, IFlowCreated
{
    public FlowCreated(IEntityId<long> entityId) : base(entityId)
    {
    }
}
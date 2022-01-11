using StateManagement.Domain.Flow.DomainEvents;
using StateManagement.SharedKernel;

namespace StateManagement.Domain.Flow;

public class FlowAggregate : AggregateRoot<long>
{
    private FlowAggregate() { }

    public FlowAggregate(string flowName)
    {
        FlowName = flowName;
        Events.Add(new FlowCreated(EntityId));
    }

    public string FlowName { get; set; }

    public List<StateEntity> States { get; private set; } = new();
}


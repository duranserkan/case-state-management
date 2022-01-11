using StateManagement.Domain.Flow;

namespace StateManagement.Application.Flow.Models;

public class FlowModel
{
    public FlowModel(FlowAggregate aggregate)
    {
        Name = aggregate.FlowName;
        Id = aggregate.Id;
        States = aggregate.States.Select(stateEntity => new StateModel(stateEntity)).ToArray();
    }

    public string Name { get; }
    public long Id { get; }
    public IReadOnlyList<StateModel> States { get; }
}
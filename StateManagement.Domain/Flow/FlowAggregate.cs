using System.ComponentModel.DataAnnotations;
using StateManagement.Domain.Flow.DomainEvents;
using StateManagement.SharedKernel;

namespace StateManagement.Domain.Flow;

public class FlowAggregate : AggregateRoot<long>
{
    private FlowAggregate() { }

    public FlowAggregate(string flowName, List<StateEntity> states)
    {
        FlowName = flowName;

        AddStates(states);
        Events.Add(new FlowCreated(EntityId));
    }

    public string FlowName { get; set; }

    public List<StateEntity> States { get; private set; } = new();

    //Todo:write unit tests
    public void AssignTaskToState(long stateId, long taskId)
    {
        //Todo: validate task transitions etc
        var state = States.SingleOrDefault(s => s.Id == stateId);
        if (state == null) throw new ValidationException("State not found");

        var previousTaskState = States.SingleOrDefault(s => s.TaskIds.Contains(taskId));

        if (previousTaskState != null)
        {
            if (!state.IsAdjacentState(previousTaskState)) throw new ValidationException();
            previousTaskState.TaskIds.Remove(taskId);
        }

        state.TaskIds.Add(taskId);
    }

    //Todo:write unit tests
    public void ChangeStateOrder(Dictionary<long, byte> stateOrders)
    {
        if (stateOrders.Count != States.Count) throw new ValidationException();

        //duplicate order validation
        var newOrderCount = stateOrders.Select(x => x.Value).Distinct().Count();
        if (States.Count != newOrderCount) throw new ValidationException();

        var minOrder = stateOrders.Select(x => x.Value).Min();
        if (minOrder != 1) throw new ValidationException();

        var maxOrder = stateOrders.Select(x => x.Value).Max();
        if (maxOrder != States.Count) throw new ValidationException();

        foreach (var state in States)
        {
            if (state.Order > States.Count) throw new ValidationException();
            if (!stateOrders.TryGetValue(state.Id, out var newOrder)) throw new ValidationException();

            state.Order = newOrder;
        }
    }

    private void AddStates(List<StateEntity> states)
    {
        //Todo:validate state order etc
        States = states;
    }
}


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




    private void AddStates(List<StateEntity> states)
    {
        //Todo:validate state order etc
        States = states;
    }
}


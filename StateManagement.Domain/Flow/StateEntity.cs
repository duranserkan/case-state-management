using System.ComponentModel.DataAnnotations;
using StateManagement.SharedKernel;

namespace StateManagement.Domain.Flow;

public class StateEntity : Entity<long>
{
    private StateEntity() { }

    public StateEntity(string name)
    {
        Name = name;
    }

    public long FlowId { get; private set; }
    public string Name { get; private set; }
    public byte Order { get; set; }

    public List<long> TaskIds { get; set; }

    public bool IsAdjacentState(StateEntity otherState)
    {
        if (otherState == null) throw new ValidationException();

        var isAdjacentState = Order + 1 == otherState.Order || Order - 1 == otherState.Order;

        return isAdjacentState;
    }
}
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
}
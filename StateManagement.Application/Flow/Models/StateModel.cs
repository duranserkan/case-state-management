using StateManagement.Domain.Flow;

namespace StateManagement.Application.Flow.Models;

public class StateModel
{
    public StateModel(StateEntity entity)
    {
        Name = entity.Name;
    }
    
    public string Name { get; }
}